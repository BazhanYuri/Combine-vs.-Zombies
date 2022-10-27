using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    public event Action GameStarted;



    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGame);
    }
    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }
    private void StartGame()
    {
        GameStarted?.Invoke();
    }
}
