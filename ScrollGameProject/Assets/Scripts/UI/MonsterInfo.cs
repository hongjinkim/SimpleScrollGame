using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI healthText;

    [Header("Image")]
    public Image profile;

    public void UpdateMonsterInfo(Dictionary<string, object> message)
    {
        if (message.ContainsKey("idx"))
            profile.sprite = GameDataManager.instance.MonsterSprites[(int)message["idx"]];
        else
            profile.sprite = null;
        if (message.ContainsKey("name"))
            nameText.text = "�̸� : " + message["name"];
        else
            nameText.text = "�̸� : ";
        if (message.ContainsKey("grade"))
            gradeText.text = "��� : " + message["grade"];
        else
            gradeText.text = "��� : ";
        if (message.ContainsKey("speed"))
            speedText.text = "�ӵ� : " + message["speed"].ToString();
        else
            speedText.text = "�ӵ� : ";
        if (message.ContainsKey("health"))
            healthText.text = "ü�� : " + (string)message["health"].ToString();
        else
            healthText.text = "ü�� : ";

        gameObject.SetActive(true);

    }
}
