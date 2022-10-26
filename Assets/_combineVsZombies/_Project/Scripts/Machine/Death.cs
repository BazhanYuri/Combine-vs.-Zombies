using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _isCanDead; 
    public event Action onDead;

    private bool _isDead = false;

    public bool IsDead { get => _isDead; }

    public void Dead()
    {
        if (_isCanDead == true)
        {
            onDead?.Invoke();
            _gameManager.LevelCompleted();
             _isDead = true;
        }
    }
}
