using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingManager : Singleton<PoolingManager>
{
    private class Pool <T> where T : Component 
    {
        private T prefab;
        public IObjectPool<T> objPool;
        private Transform objContainer;

        public Pool(T prefab)
        {
            this.prefab = prefab;

            if (objContainer == null)
            {
                objContainer = new GameObject($"{prefab.name}_Pool").transform;
            }

            objPool = new ObjectPool<T>(CreatePooledItem, OnGet, OnRelease,
            OnDestroyPoolObject);
        }

        // »ý¼º
        private T CreatePooledItem()
        {
            T instantiateObj = Instantiate(prefab, objContainer);
            instantiateObj.name = prefab.name;
            return instantiateObj;
        }

        private void OnGet(T poolGo)
        {
            poolGo.gameObject.SetActive(true);
        }

        private void OnRelease(T poolGo)
        {
            poolGo.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(T poolGo)
        {
            Destroy(poolGo.gameObject);
        }

        public T Get()
        {
            return objPool.Get();
        }

        public void Release(T obj)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return;
            }

            objPool.Release(obj);
        }
    }

    public Dictionary<string, object> poolList = new Dictionary<string, object>();

    public void CreatePool<T>(T poolObj) where T : Component
    {
        var pool = new Pool<T>(poolObj);
        poolList.Add(poolObj.name, pool);
    }

    public T Pop<T>(T poolObj) where T : Component
    {
        if (!poolList.ContainsKey(poolObj.name))
            CreatePool<T>(poolObj);

        return ((Pool<T>)poolList[poolObj.name]).Get();
    }

    public void Push<T>(T poolObj) where T : Component
    {
        ((Pool<T>) poolList[poolObj.name]).Release(poolObj);
    }
}
