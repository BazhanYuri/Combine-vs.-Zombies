using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private Improvement _improvement;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private Death _death;

    public Improvement Improvement { get => _improvement; }
    public Death Death { get => _death;}

    private void Start()
    {
        _forwardMovement.SetSpeed(_improvement.CurrentSpeed);
    }
}
