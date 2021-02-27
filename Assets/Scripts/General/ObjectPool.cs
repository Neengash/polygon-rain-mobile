using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] int poolSize;
    [SerializeField] PoolableObject prefab;
    [SerializeField]
    List<PoolableObject> pool;

    void Start()
    {
        pool = new List<PoolableObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateElement();
        }
    }

    protected PoolableObject CreateElement() {
        PoolableObject element = Instantiate(prefab);
        element.setPool(this);
        element.loadReferences();
        element.gameObject.SetActive(false);
        // pool.Add(element); // poolableObjects are added ondisable
        return element;
    }

    public void addToPool(PoolableObject element) {
        pool.Add(element);
    }

    public PoolableObject getNext() {
        PoolableObject element = null;
        if (pool.Count > 0) {
            element = pool[0];
        } else {
            element = CreateElement();
        }
        pool.RemoveAt(0);
        return element;
    }
}
