using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class UIManager : MonoSingleton<UIManager>
{
    public MonsterInfo monsterInfo;

    private void Start()
    {
        monsterInfo.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventType.MonsterDied, OnMonsterDied);
        EventManager.StartListening(EventType.MonsterSelected, OnMonsterSelected);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventType.MonsterDied, OnMonsterDied);
        EventManager.StopListening(EventType.MonsterSelected, OnMonsterSelected);
    }

    public LayerMask monsterCollisionLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (IsLayerMatched(hit.collider.gameObject.layer, gameObject.layer))
                {
                    hit.collider.gameObject.GetComponent<Monster>().OnMonsterClicked();
                }
            }
            else if (monsterInfo.gameObject.activeSelf)
            {
                monsterInfo.gameObject.SetActive(false);
            }
        }
    }

    private void OnMonsterSelected(Dictionary<string, object> message = null)
    {
        monsterInfo.UpdateMonsterInfo(message);
    }

    private void OnMonsterDied(Dictionary<string, object> message = null)
    {
        monsterInfo.gameObject.SetActive(false);
    }

}
