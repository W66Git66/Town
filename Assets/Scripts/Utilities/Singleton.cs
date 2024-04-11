using UnityEngine;

public class Singleton<T>: MonoBehaviour where T :Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
            instance = this as T;
    }
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static bool isInstance
    {
        get { return instance != null; }
    }
}

public class SingletonPeisistant<T> : Singleton<T> where T : SingletonPeisistant<T>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

}

