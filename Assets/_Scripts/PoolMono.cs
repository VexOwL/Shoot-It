using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab {get;}
    public bool AutoExpand {get; set;}
    public Transform _container {get;}
    private List<T> _pool;

    public PoolMono(T prefab, int size)
    {
        Prefab = prefab;
        _container = null;

        CreatePool(size);
    }

    public PoolMono(T prefab, int size, Transform container)
    {
        Prefab = prefab;
        _container = container;

        CreatePool(size);
    }

    private void CreatePool(int size)
    {
        _pool = new List<T>();

        for(int i = 0; i < size; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = Object.Instantiate(Prefab, _container);
        createObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createObject);
        return createObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var mono in _pool)
        {
            if(!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(HasFreeElement(out var element))
            return element;

        if(AutoExpand)
            return CreateObject(true);

        throw new Exception($"No free elements in pool of type {typeof(T)}");
    }
}
