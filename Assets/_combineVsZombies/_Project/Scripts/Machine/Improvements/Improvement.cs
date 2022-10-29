using UnityEngine;
using Sirenix.OdinInspector;

public class Improvement : MonoBehaviour
{
    [Tooltip("Meters per second")]
    [SerializeField] private float _standartSpeed;
    [Tooltip("In kilograms")]
    [SerializeField] private float _standartSawPower; //kg
    [Tooltip("In kilograms per second")]
    [SerializeField] private float _standartCoolRate; //kgPerSecond
    [Space]
    [SerializeField][ReadOnly] private Improver _improver;

    [Button]
    private void SetRefs()
    {
        _improver = GetComponent<Improver>();
    }


    private float _currentSpeed;
    private float _currentSawPower;
    private float _currentCoolRate;

    public float CurrentSpeed { get => _currentSpeed;}
    public float CurrentSawPower { get => _currentSawPower; }
    public float CurrentCoolRate { get => _currentCoolRate; }

    private void OnEnable()
    {
        CalculateImprovementsFromImprover();
        print("After calculating improving: ");
        print("Speed " + _currentSpeed);
        print("Saw Power " + _currentSawPower);
        print("Cool rate " + _currentCoolRate);
    }
    private void CalculateImprovementsFromImprover()
    {
        _currentSpeed = _improver.CalculateValue(ImproveType.Engine, _standartSpeed);
        _currentSawPower = _improver.CalculateValue(ImproveType.Engine, _standartSawPower);
        _currentCoolRate = _improver.CalculateValue(ImproveType.Engine, _standartCoolRate);
    }
}
