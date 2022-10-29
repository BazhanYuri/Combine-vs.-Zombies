using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _mainMenuCamera;
    [SerializeField] private Button _startGameButton;

    public event Action GameStarted;



    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _mainMenuCamera.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(StartGame);
    }
    private void StartGame()
    {
        GameStarted?.Invoke();
        _mainMenuCamera.gameObject.SetActive(false);
    }
}
