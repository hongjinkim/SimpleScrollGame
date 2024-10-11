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
            nameText.text = "이름 : " + message["name"];
        else
            nameText.text = "이름 : ";
        if (message.ContainsKey("grade"))
            gradeText.text = "등급 : " + message["grade"];
        else
            gradeText.text = "등급 : ";
        if (message.ContainsKey("speed"))
            speedText.text = "속도 : " + message["speed"].ToString();
        else
            speedText.text = "속도 : ";
        if (message.ContainsKey("health"))
            healthText.text = "체력 : " + (string)message["health"].ToString();
        else
            healthText.text = "체력 : ";

        gameObject.SetActive(true);

    }
}
