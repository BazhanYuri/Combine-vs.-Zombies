using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Transform _deadCamera;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _isCanDead;
    [SerializeField] private Transform _gameProcessUI;

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
            _gameProcessUI.gameObject.SetActive(false);
            _deadCamera.gameObject.SetActive(true);
        }
    }
}
