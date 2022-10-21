using UnityEngine;

public class MachineForwardMovement : ForwardMovement
{
    [SerializeField] private Machine _machine;



    private void OnEnable()
    {
        _machine.Death.onDead += StopMove;
    }
    private void OnDisable()
    {
        _machine.Death.onDead -= StopMove;
    }
}
