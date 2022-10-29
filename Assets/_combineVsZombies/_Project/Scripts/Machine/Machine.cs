using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private Improvement _improvement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private ScreenTapHandler _screenTapHandler;
    [SerializeField] private Death _death;

    public Improvement Improvement { get => _improvement; }
    public Death Death { get => _death;}
    public GameStarter GameStarter { get => _gameStarter;}

    private void OnEnable()
    {
        _gameStarter.GameStarted += _forwardMovement.StartMove;
        _gameStarter.GameStarted += _screenTapHandler.StartMove;
    }
    private void OnDisable()
    {
        _gameStarter.GameStarted -= _forwardMovement.StartMove;
        _gameStarter.GameStarted -= _screenTapHandler.StartMove;
    }
    private void Start()
    {
        _forwardMovement.SetSpeed(_improvement.CurrentSpeed);
    }
}
