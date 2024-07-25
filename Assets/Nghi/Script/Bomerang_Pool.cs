using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang_Pool : MonoBehaviour
{
    public GameObject objectToPool;
    public int amountToPool;
    public List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0;i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject gameObject = Instantiate(objectToPool);
        gameObject.SetActive(false);
        pooledObjects.Add(gameObject);
        return gameObject;
    }
}
