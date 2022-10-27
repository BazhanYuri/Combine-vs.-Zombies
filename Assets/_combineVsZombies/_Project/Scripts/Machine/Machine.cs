using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private Improvement _improvement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private Death _death;

    public Improvement Improvement { get => _improvement; }
    public Death Death { get => _death;}


    private void OnEnable()
    {
        _gameStarter.GameStarted += _forwardMovement.StartMove;
        _gameStarter.GameStarted += _sideMovement.StartMove;
    }
    private void OnDisable()
    {
        _gameStarter.GameStarted -= _forwardMovement.StartMove;
        _gameStarter.GameStarted -= _sideMovement.StartMove;
    }
    private void Start()
    {
        _forwardMovement.SetSpeed(_improvement.CurrentSpeed);
    }
}
