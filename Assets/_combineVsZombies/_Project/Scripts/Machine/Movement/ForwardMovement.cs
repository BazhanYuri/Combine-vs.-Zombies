using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _visual;
    [SerializeField] private Vector3 _moveVector;



    private void Update()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        _visual.position += _moveVector * _speed * Time.deltaTime;
    }
}
