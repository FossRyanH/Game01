using UnityEngine;
using UnityEngine.PlayerLoop;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindFirstObjectByType(typeof(T));
                if (_instance == null)
                {
                    SetupInstance();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        RemoveDuplicates();
        Init();
    }
    static void SetupInstance()
    {
        _instance = (T)FindFirstObjectByType(typeof(T));

        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            _instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }

    void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Init() {}
}
