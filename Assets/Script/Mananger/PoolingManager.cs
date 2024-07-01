using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingManager : Singleton<PoolingManager>
{
    public int defaultCapacity = 10;
    public int maxPoolSize = 15;
    public GameObject defalutBullet;

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        for(int i = 0; i < defaultCapacity; i++)
        {
            DefaultBullet defaultB = CreatePooledItem().GetComponent<DefaultBullet>();
            defaultB.Pool.Release(defaultB.gameObject);
        }
    }

    // »ý¼º
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(defalutBullet);
        poolGo.GetComponent<DefaultBullet>().Pool = this.Pool;
        return poolGo;
    }
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}
