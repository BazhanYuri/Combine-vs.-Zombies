using UnityEngine;

public class PassedDistanceManager : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private Death _death;
    [SerializeField] private Transform _machineVisual;
    [SerializeField] private TextUpdatedTrigger _textUpdatedTrigger;


    private int _passedDistance;

    public int PassedDistance { get => _passedDistance;}



    private void OnEnable()
    {
        _death.onDead += SetPassedDistance;
    }
    private void OnDisable()
    {
        _death.onDead -= SetPassedDistance;
    }
    private void Update()
    {
        _passedDistance = (int)_machineVisual.position.z;
        _textUpdatedTrigger.InvokeUpdated(_passedDistance);
    }
    private void SetPassedDistance()
    {
        _moneyManager.PassedDistance = _passedDistance;
    }
}
