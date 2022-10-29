using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField, ReadOnly] private Camera m_MainCamera;
    [SerializeField, ReadOnly] private CinemachineVirtualCamera _mainMenuCamera;

    public Camera MainCamera => m_MainCamera;

    [Button]
    private void setRef()
    {
        m_MainCamera = transform.FindDeepChild<Camera>("Main Camera");
        _mainMenuCamera = transform.FindDeepChild<CinemachineVirtualCamera>("MainMenuCamera");
    }



    private void OnEnable()
    {
        _mainMenuCamera.gameObject.SetActive(true);
        _gameStarter.GameStarted += ShowGameProcessCamera;
    }
    private void OnDisable()
    {
        _gameStarter.GameStarted -= ShowGameProcessCamera;
    }
    private void ShowGameProcessCamera()
    {
        _mainMenuCamera.gameObject.SetActive(false);
    }
}
