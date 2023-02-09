using System;
using System.Collections.Generic;
namespace Kurisu.AkiDialogue.Utility
{
    /// <summary>
    /// 参考自Joker JKFrame https://github.com/Joker-YF/JKFrame
    /// </summary>
    public class PoolManager
    {
        private static PoolManager instance;
        public static PoolManager Instance=>instance??new PoolManager();
        public Dictionary<string, ObjectPoolData> objectPoolDic = new Dictionary<string, ObjectPoolData>();
        public T GetObject<T>() where T : class, new()
        {
            T obj;
            if (CheckObjectCache<T>())
            {
                string name = typeof(T).FullName;
                obj = (T)objectPoolDic[name].GetObj();
                return obj;
            }
            else
            {
                return new T();
            }
        }
        public object GetObject(string objectName) 
        {
            object obj;
            if (CheckObjectCache(objectName))
            {
                obj = objectPoolDic[objectName].GetObj();
                return obj;
            }
            else
            {
                return null;
            }
        }

        public void PushObject(object obj)
        {
            string name = obj.GetType().FullName;
            // 现在有没有这一层
            if (objectPoolDic.ContainsKey(name))
            {
                objectPoolDic[name].PushObj(obj);
            }
            else
            {
                objectPoolDic.Add(name, new ObjectPoolData(obj));
            }
        }
        public void PushObject(object obj,string overrideName)
        {
            // 现在有没有这一层
            if (objectPoolDic.ContainsKey(overrideName))
            {
                objectPoolDic[overrideName].PushObj(obj);
            }
            else
            {
                objectPoolDic.Add(overrideName, new ObjectPoolData(obj));
            }
        }

        private bool CheckObjectCache<T>()
        {
            string name = typeof(T).FullName;
            return objectPoolDic.ContainsKey(name) && objectPoolDic[name].poolQueue.Count > 0;
        }
        private bool CheckObjectCache(string objectName)
        {
            return objectPoolDic.ContainsKey(objectName) && objectPoolDic[objectName].poolQueue.Count > 0;
        }
       
        public void Clear()
        {
            objectPoolDic.Clear();
        }

        public void ClearObject<T>()
        {
            objectPoolDic.Remove(typeof(T).FullName);
        }
        public void ClearObject(Type type)
        {
            objectPoolDic.Remove(type.FullName);
        }
    }
    public class ObjectPoolData
    {
        public ObjectPoolData(object obj)
        {
            PushObj(obj);
        }
        public Queue<object> poolQueue = new Queue<object>();
        public void PushObj(object obj)
        {
            poolQueue.Enqueue(obj);
        }
        public object GetObj()
        {
            return poolQueue.Dequeue();
        }
    }
    public static class PoolExtension
    {
        public static void ObjectPushPool(this object obj)
        {
            PoolManager.Instance.PushObject(obj);
        }
        public static void ObjectPushPool(this object obj,string overrideName)
        {
            PoolManager.Instance.PushObject(obj,overrideName);
        }
    }
}
