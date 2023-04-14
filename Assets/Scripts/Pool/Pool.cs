using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject poolObject;
    [SerializeField] private Transform container;
    [SerializeField] private int minObjectCount;
    [SerializeField] private int maxObjectCount;
    [SerializeField] private bool createObjectsAutomatically;

    private List<PoolObject> poolObjectsList;

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        poolObjectsList = new List<PoolObject>(minObjectCount);

        for (int i = 0; i < minObjectCount; i++)
        {
            CreateObject(false);
        }
    }

    private PoolObject CreateObject(bool DefaultActive)
    {
        var CreatedObject = Instantiate(poolObject, container);
        CreatedObject.gameObject.SetActive(DefaultActive);

        poolObjectsList.Add(CreatedObject);
        return CreatedObject;
    }

    public bool TryGetObject(out PoolObject Object)
    {
        foreach (var item in poolObjectsList)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                Object = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        Object = null;
        return false;
    }

    public PoolObject GetFreeObject()
    {
        if (TryGetObject(out PoolObject Object))
        {
            return Object;
        }

        if (poolObjectsList.Count < maxObjectCount)
        {
            return CreateObject(false);
        }
        else if (createObjectsAutomatically)
        {
            maxObjectCount++;
            return CreateObject(false);
        }

        Debug.LogError("Pool is Full!");
        return null;
    }

    public PoolObject GetFreeObject(Vector3 ObjectPotition)
    {
        PoolObject Object = GetFreeObject();
        Object.transform.position = ObjectPotition;
        Object.gameObject.SetActive(true);
        return Object;
    }
    public PoolObject GetFreeObject(Vector3 ObjectPotition, Transform Parent)
    {
        PoolObject Object = GetFreeObject();
        Object.transform.position = ObjectPotition;
        Object.transform.SetParent(Parent);
        Object.gameObject.SetActive(true);
        Object.PoolTransform = gameObject.transform;
        return Object;
    }
}
