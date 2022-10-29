using UnityEngine;

public class SawRotation : MonoBehaviour
{
    [SerializeField] private SawPowerEngine _sawPowerEngine;
    [SerializeField] private Transform _sawVisual;
    [SerializeField] private Machine _machine;

    [SerializeField] private Vector3 _rotationVector;
    [SerializeField] private float _standartSpeed;


    private float _currentSawPower;

    public float CurrentSawPower {set => _currentSawPower = value; }

    private void FixedUpdate()
    {
        if (_machine.Death.IsDead == false)
        {
            _sawVisual.Rotate(_rotationVector * CalculateSpeed());
        }
    }
    private float CalculateSpeed()
    {
        float speed = _standartSpeed * (_currentSawPower * 0.1f) * ((100 - _sawPowerEngine.PercentageOfSawWeightFull) / 100f); 
        if (speed < 0)
        {
            speed = 0;
        }
        return speed;
    }
}
