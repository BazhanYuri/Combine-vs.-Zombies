using UnityEngine;

public class Improvement : MonoBehaviour
{
    [SerializeField] private float _standartSpeed; //metersPerSecond
    [SerializeField] private float _improveSpeedIndex; 
    [Space]
    [SerializeField] private int _standartSawPower; //kg
    [SerializeField] private int _improveSawIndex;
    [Space]
    [SerializeField] private int _standartCoolRate; //kgPerSecond
    [SerializeField] private int _improveCoolRateIndex; 

    private float _currentSpeed;
    private int _currentSawPower;
    private int _currentCoolRate;

    public float CurrentSpeed { get => _currentSpeed;}
    public int CurrentSawPower { get => _currentSawPower; }
    public int CurrentCoolRate { get => _currentCoolRate; }

    private void OnEnable()
    {
        _currentSpeed = _standartSpeed;
        _currentSawPower = _standartSawPower;
        _currentCoolRate = _standartCoolRate;
    }
}
