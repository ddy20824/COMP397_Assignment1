using UnityEngine;

namespace Platformer397
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static bool AutoUnparentOnAwake = true;
        protected static T instance;
        public static bool HasInstance => HasInstance ? instance : null;
        public static T TryGetInstance() => HasInstance ? instance : null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Generated");
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (AutoUnparentOnAwake)
            {
                transform.SetParent(null);
            }
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
