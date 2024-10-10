using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI grade;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI health;

    public void UpdateMonsterInfo(Dictionary<string, object> message)
    {
        if (message.ContainsKey("name"))
            name.text = "�̸� : " + message["name"];
        else
            name.text = "�̸� : ";
        if (message.ContainsKey("grade"))
            grade.text = "��� : " + message["grade"];
        else
            grade.text = "��� : ";
        if (message.ContainsKey("speed"))
            speed.text = "�ӵ� : " + message["speed"].ToString();
        else
            speed.text = "�ӵ� : ";
        if (message.ContainsKey("health"))
            health.text = "ü�� : " + (string)message["health"].ToString();
        else
            health.text = "ü�� : ";

        gameObject.SetActive(true);

    }
}
