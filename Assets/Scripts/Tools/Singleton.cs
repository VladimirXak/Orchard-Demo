using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = $"[SINGLETON] {typeof(T)}";
                }

                _instance.gameObject.transform.SetParent(null, false);
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();
        }
        else
        {
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        MonoBehaviour[] scripts = Object.FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}
