using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _visual;
    [SerializeField] private Vector3 _moveVector;


    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void StopMove()
    {
        _speed = 0;
    }

    
    private void Update()
    {

        MoveForward();
    }
    private void MoveForward()
    {
        _visual.position += _moveVector * _speed * Time.deltaTime;
    }
}
