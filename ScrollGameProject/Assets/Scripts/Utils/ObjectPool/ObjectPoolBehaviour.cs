using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBehaviour : MonoBehaviour
{

    public Transform parentTransform;

    public ObjectPoolData data;

    private List<GameObject> pooledObjects;

    void Awake()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < data.amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(data.objectToPool, parentTransform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (data.shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(data.objectToPool, parentTransform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;

    }

    public void ClearObjects()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }

}