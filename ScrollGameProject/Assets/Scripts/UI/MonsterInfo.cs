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
            name.text = "이름 : " + message["name"];
        else
            name.text = "이름 : ";
        if (message.ContainsKey("grade"))
            grade.text = "등급 : " + message["grade"];
        else
            grade.text = "등급 : ";
        if (message.ContainsKey("speed"))
            speed.text = "속도 : " + message["speed"].ToString();
        else
            speed.text = "속도 : ";
        if (message.ContainsKey("health"))
            health.text = "체력 : " + (string)message["health"].ToString();
        else
            health.text = "체력 : ";

        gameObject.SetActive(true);

    }
}
