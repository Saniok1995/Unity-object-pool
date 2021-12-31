using UnityEngine;

namespace ObjectPool
{
    public class BasePoolObject : MonoBehaviour, IPoolObject
    {
        public virtual Transform GetTransform()
        {
            return transform;
        }
        
        public virtual void OnReturnToPool()
        {
            
        }

        public virtual void OnGetFromPool()
        {
            
        }

        public virtual void OnCreateInPool()
        {
            
        }
    }
}


