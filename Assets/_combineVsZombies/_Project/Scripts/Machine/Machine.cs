using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private Improvement _improvement;
    [SerializeField] private ForwardMovement _forwardMovement;

    public Improvement Improvement { get => _improvement; }

    private void Start()
    {
        _forwardMovement.SetSpeed(_improvement.CurrentSpeed);
    }
}
