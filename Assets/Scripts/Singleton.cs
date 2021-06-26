using UnityEngine;

namespace Framework
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        [RuntimeInitializeOnLoadMethod]
        static void RunOnStart()
        {
            Application.quitting += () => applicationIsQuitting = true;
        }

        private static bool applicationIsQuitting = false;
        protected static bool cleanupOnQuit = true;
        
        private static T instance;

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting  && cleanupOnQuit)
                {
                    Destroy(instance);
                    instance = null;
                    return null;
                }
                
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject {name = typeof(T).Name};
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                //Destroy(gameObject);
                // Don't destroy the gameobject because it could be instantiated with the Instance getter
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }
    }
}