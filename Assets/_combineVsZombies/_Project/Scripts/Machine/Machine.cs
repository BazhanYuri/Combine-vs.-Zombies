using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private Improvement _improvement;
    [SerializeField] private ForwardMovement _forwardMovement;



    private void Start()
    {
        _forwardMovement.SetSpeed(_improvement.CurrentSpeed);
    }
}
