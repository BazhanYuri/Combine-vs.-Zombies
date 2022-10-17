using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual void Awake()
    {
    }
}
