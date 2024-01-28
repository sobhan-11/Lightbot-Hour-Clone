using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ObjectPool : MonoBehaviour
{
    [Header("Config"),Space]
    public GameObject objectToPool;
    public bool isAutoCreate;
    public int size;

    private void OnEnable()
    {
        if(isAutoCreate)
            CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < size; i++)
        {
            var GO=Instantiate(objectToPool, transform);
            GO.GetComponent<IPooledObject>().SetPool(this);
            GO.SetActive(false);
        }
    }
    
    public void CreatePool(Vector3 scale)
    {
        for (int i = 0; i < size; i++)
        {
            var GO=Instantiate(objectToPool, transform);
            GO.transform.localScale = scale;
            GO.GetComponent<IPooledObject>().SetPool(this);
            GO.SetActive(false);
        }
    }

    public T GetObject<T>() where T : MonoBehaviour
    {
        var GO = transform.GetChild(0);
        if (GO)
        {
            GO.transform.SetParent(null);
            return GO.GetComponent<T>();
        }

        return null;
    }
    
    
}
