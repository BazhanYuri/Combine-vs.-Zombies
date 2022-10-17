using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _visual;
    [SerializeField] private Vector3 _moveVector;


    private bool _isCanMove = true;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void StopMove()
    {
        _isCanMove = false;
    }

    private void Update()
    {
        if (_isCanMove == true)
        {
            MoveForward();
        }
    }
    private void MoveForward()
    {
        _visual.position += _moveVector * _speed * Time.deltaTime;
    }
}
