using UnityEngine;

public class PassedDistanceManager : MonoBehaviour
{
    [SerializeField] private Transform _machineVisual;


    private int _passedDistance;

    public int PassedDistance { get => _passedDistance;}

    private void Update()
    {
        _passedDistance = (int)_machineVisual.position.z;
    }
}
