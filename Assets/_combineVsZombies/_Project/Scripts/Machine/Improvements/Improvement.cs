using UnityEngine;
using Sirenix.OdinInspector;

public class Improvement : MonoBehaviour
{
    [SerializeField] private Machine _machine;
    [Tooltip("Meters per second")]
    [SerializeField] private float _standartSpeed;
    [Tooltip("In kilograms")]
    [SerializeField] private float _standartSawPower; //kg
    [Tooltip("In kilograms per second")]
    [SerializeField] private float _standartCoolRate; //kgPerSecond
    [Space]
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField][ReadOnly] private Improver _improver;

    [Button]
    private void SetRefs()
    {
        _improver = GetComponent<Improver>();
    }


    private float _currentSpeed;
    private float _currentSawPower;
    private float _currentCoolRate;


    private void OnEnable()
    {
        _gameStarter.GameStarted += CalculateImprovementsFromImprover;
       
    }
    private void OnDisable()
    {
        _gameStarter.GameStarted -= CalculateImprovementsFromImprover;
    }
    private void CalculateImprovementsFromImprover()
    {
        _currentSpeed = _improver.CalculateValue(ImproveType.Engine, _standartSpeed);
        _currentSawPower = _improver.CalculateValue(ImproveType.Engine, _standartSawPower);
        _currentCoolRate = _improver.CalculateValue(ImproveType.Engine, _standartCoolRate);

        _machine.SawRotation.CurrentSawPower = _currentSawPower;
        _machine.ForwardMovement.SetSpeed(_currentSpeed);
        _machine.SawPowerEngine.CurrentCoolRate = _currentCoolRate;
        _machine.SawPowerEngine.CurrentSawPower = _currentSawPower;

    }
}
