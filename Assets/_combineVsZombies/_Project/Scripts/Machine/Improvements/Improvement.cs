using UnityEngine;

public class Improvement : MonoBehaviour
{
    [SerializeField] private float _standartSpeed;
    [SerializeField] private float _improveIndex;

    private float _currentSpeed;

    public float CurrentSpeed { get => _currentSpeed;}

    private void OnEnable()
    {
        _currentSpeed = _standartSpeed;
    }
}
