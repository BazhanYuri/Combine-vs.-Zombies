using UnityEngine;

public interface ISingelton
{
    bool IsSingletonLogsEnabled { get; set; }
}

public class Singleton<T> : SingletonBase<Singleton<T>>, ISingelton where T : MonoBehaviour, ISingelton
{
    private static T _instance;

    public bool DontDestroyLoad;

    public bool IsSingletonLogsEnabled { get => _isSingletonLogsEnabled; set => _isSingletonLogsEnabled = value; }

    [SerializeField, HideInInspector]
    private bool _isSingletonLogsEnabled = false;

    protected bool _isDestroyed = false;

    protected static bool applicationIsQuittingFlag = false;
    protected static bool applicationIsQuitting = false;

    public static bool IsInstanceNull { get { return _instance == null; } }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    if (applicationIsQuitting && Application.isPlaying)
                    {
                        Debug.LogWarning("[Singleton] IsQuitting Instance '" + typeof(T) + "' is null, returning.");
                        return _instance;
                    }
                    else
                    {
                        //create a new gameObject, if Instance isn't found
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "[Singleton] " + typeof(T).ToString();
                        Debug.Log("[Singleton] An instance of '" + typeof(T) + "' was created: " + singleton);
                    }
                }
                else
                {
                    if (_instance.IsSingletonLogsEnabled)
                    {
                        Debug.Log("[Singleton] Found instance of '" + typeof(T) + "': " + _instance.gameObject.name);
                    }
                }
            }

            return _instance;
        }

    }

    protected sealed override void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>();
            if (DontDestroyLoad)
            {
                setDontDestroyOnLoad();
            }
            OnAwakeEvent();
        }
        else
        {
            if (this == _instance)
            {
                if (DontDestroyLoad)
                {
                    setDontDestroyOnLoad();
                }
                OnAwakeEvent();
            }
            else
            {
                _isDestroyed = true;
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual void OnAwakeEvent() { }

    public virtual void Start() { }

    public virtual void OnDisable()
    {
        #if UNITY_EDITOR
        applicationIsQuitting = applicationIsQuittingFlag;
        #endif
    }

    public virtual void OnDestroy()
    {
    }

    protected void setDontDestroyOnLoad()
    {
        DontDestroyLoad = true;
        if (DontDestroyLoad)
        {
            if (transform.parent != null)
            {
                transform.parent = null;
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed,
    /// it will create a buggy ghost object that will stay on the Editor scene
    /// even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
#if UNITY_EDITOR
    public virtual void OnApplicationQuit()
    {
        applicationIsQuittingFlag = true;
    }
#endif


}
