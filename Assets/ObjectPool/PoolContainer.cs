using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class PoolContainer<T> where T : IPoolObject
    {
        private readonly Stack<T> pool = new Stack<T>();
        private readonly Transform poolParent;
        private readonly GameObject prefab;

        public PoolContainer(GameObject prefab, Transform poolParent, int preCreatedObjectsCount = 0)
        {
            this.prefab = prefab;
            this.poolParent = poolParent;

            for (int i = 0; i < preCreatedObjectsCount; i++)
            {
                var instance = CreateInstance();
                pool.Push(instance);
            }
        }

        public T GetObjectFromPool()
        {
            if (pool.Count >= 1)
            {
                var poolObject = pool.Pop();
                poolObject.OnGetFromPool();
                return poolObject;
            }
            
            var instance = CreateInstance();
            instance.OnCreateInPool();
            return instance;
        }

        public void ReturnObjectToPool(T poolObject)
        {
            poolObject.GetTransform().SetParent(poolParent);
            poolObject.OnReturnToPool();
            pool.Push(poolObject);
        }
        
        public void Clear()
        {
            foreach (var poolObject in pool)
            {
                Object.Destroy(poolObject.GetTransform().gameObject);
            }
            
            pool.Clear();
        }

        private T CreateInstance()
        {
            var obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            var component = obj.GetComponent<T>();
            obj.transform.SetParent(poolParent);
            component.OnCreateInPool();
            return component;
        }
    }
}
