using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SampleMonsterData", menuName = "ScriptableObjects/SampleMonsterData")]
public class SampleMonsterData : ScriptableObject
{
    [Serializable]
    public class DataEntry
    {
        public string Name;
        public string Grade;
        public float Speed;
        public int Health;
    }

    public DataEntry[] entries;
}
