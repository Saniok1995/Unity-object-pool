using UnityEngine;

namespace ObjectPool
{
    public interface IPoolObject
    {
        void OnReturnToPool();
        void OnGetFromPool();
        void OnCreateInPool();
        Transform GetTransform();
    }
}


