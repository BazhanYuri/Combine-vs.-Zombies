using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private bool _isCanDead; 
    public event Action onDead;




    public void Dead()
    {
        if (_isCanDead == true)
        {
            onDead?.Invoke();
        }
    }
}
