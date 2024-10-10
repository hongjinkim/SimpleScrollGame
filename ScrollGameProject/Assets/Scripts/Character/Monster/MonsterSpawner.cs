using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public SampleMonsterData monsterData;
    public ObjectPoolBehaviour objectPool;

    private GameObject monster;
    [SerializeField] private int monsterIdx;
    private int numOfMonsters;
    private bool isMonsterAlive;

    private void Awake()
    {
        monsterIdx = 0;
        numOfMonsters = monsterData.entries.Length - 1;
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventType.MonsterDied, Spawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventType.MonsterDied, Spawn);
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn(Dictionary<string, object> message = null)
    {
        isMonsterAlive = true;
   
        monster = objectPool.GetPooledObject();
        monster.transform.position = this.transform.position;
        monster.GetComponent<Monster>().Setup(monsterIdx);
        if (monsterIdx < numOfMonsters)
            monsterIdx++;
        else
            monsterIdx = 0;
        monster.SetActive(true);
    }

}
