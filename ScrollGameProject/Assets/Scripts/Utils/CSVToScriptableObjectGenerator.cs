using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;

public class CSVToScriptableObjectGenerator : EditorWindow
{
    private string csvFilePath = "";
    private string scriptFolderPath = "Assets/Scripts/Generated";
    private string assetFolderPath = "Assets/ScriptableObjects/Generated";

    [MenuItem("Tools/CSV to ScriptableObject Generator")]
    public static void ShowWindow()
    {
        GetWindow<CSVToScriptableObjectGenerator>("CSV to SO Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV to ScriptableObject Generator", EditorStyles.boldLabel);

        csvFilePath = EditorGUILayout.TextField("CSV File Path:", csvFilePath);
        scriptFolderPath = EditorGUILayout.TextField("Script Folder Path:", scriptFolderPath);
        assetFolderPath = EditorGUILayout.TextField("Asset Folder Path:", assetFolderPath);

        if (GUILayout.Button("Generate"))
        {
            GenerateScriptableObjectFromCSV();
        }
    }

    private void GenerateScriptableObjectFromCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found!");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file is empty or contains only headers!");
            return;
        }

        string[] headers = lines[0].Split(',');
        Type[] columnTypes = InferColumnTypes(lines.Skip(1).ToArray(), headers.Length);

        string className = Path.GetFileNameWithoutExtension(csvFilePath) + "Data";
        string scriptPath = Path.Combine(scriptFolderPath, className + ".cs");

        // Generate script file
        GenerateScriptableObjectScript(scriptPath, className, headers, columnTypes);

        // Compile the newly created script
        AssetDatabase.Refresh();

        // Create ScriptableObject instance
        ScriptableObject data = ScriptableObject.CreateInstance(className);
        Type dataType = data.GetType();
        FieldInfo entriesField = dataType.GetField("entries");

        Type entryType = dataType.GetNestedType("DataEntry");
        Array entriesArray = Array.CreateInstance(entryType, lines.Length - 1);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            object entry = Activator.CreateInstance(entryType);

            for (int j = 0; j < headers.Length; j++)
            {
                FieldInfo field = entryType.GetField(headers[j]);
                field.SetValue(entry, ConvertToType(values[j], columnTypes[j]));
            }

            entriesArray.SetValue(entry, i - 1);
        }

        entriesField.SetValue(data, entriesArray);

        // Save ScriptableObject asset
        string assetPath = Path.Combine(assetFolderPath, className + ".asset");
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"ScriptableObject script created at: {scriptPath}");
        Debug.Log($"ScriptableObject asset created at: {assetPath}");
    }

    private void GenerateScriptableObjectScript(string path, string className, string[] headers, Type[] columnTypes)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using System;");
        sb.AppendLine();
        sb.AppendLine($"[CreateAssetMenu(fileName = \"{className}\", menuName = \"ScriptableObjects/{className}\")]");
        sb.AppendLine($"public class {className} : ScriptableObject");
        sb.AppendLine("{");
        sb.AppendLine("    [Serializable]");
        sb.AppendLine("    public class DataEntry");
        sb.AppendLine("    {");

        for (int i = 0; i < headers.Length; i++)
        {
            sb.AppendLine($"        public {GetTypeName(columnTypes[i])} {headers[i]};");
        }

        sb.AppendLine("    }");
        sb.AppendLine();
        sb.AppendLine("    public DataEntry[] entries;");
        sb.AppendLine("}");

        File.WriteAllText(path, sb.ToString());
    }

    private Type[] InferColumnTypes(string[] dataRows, int columnCount)
    {
        Type[] types = new Type[columnCount];

        for (int i = 0; i < columnCount; i++)
        {
            types[i] = typeof(string); // Default type

            bool canBeInt = true;
            bool canBeFloat = true;
            bool canBeBool = true;

            foreach (var row in dataRows)
            {
                string[] values = row.Split(',');
                if (values.Length <= i) continue;

                string value = values[i].Trim();

                if (canBeInt && !int.TryParse(value, out _))
                    canBeInt = false;

                if (canBeFloat && !float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                    canBeFloat = false;

                if (canBeBool && !bool.TryParse(value, out _))
                    canBeBool = false;

                if (!canBeInt && !canBeFloat && !canBeBool)
                    break;
            }

            if (canBeInt)
                types[i] = typeof(int);
            else if (canBeFloat)
                types[i] = typeof(float);
            else if (canBeBool)
                types[i] = typeof(bool);
        }

        return types;
    }

    private object ConvertToType(string value, Type type)
    {
        if (type == typeof(int))
            return int.Parse(value);
        else if (type == typeof(float))
            return float.Parse(value, CultureInfo.InvariantCulture);
        else if (type == typeof(bool))
            return bool.Parse(value);
        else
            return value;
    }

    private string GetTypeName(Type type)
    {
        if (type == typeof(int))
            return "int";
        else if (type == typeof(float))
            return "float";
        else if (type == typeof(bool))
            return "bool";
        else
            return "string";
    }
}