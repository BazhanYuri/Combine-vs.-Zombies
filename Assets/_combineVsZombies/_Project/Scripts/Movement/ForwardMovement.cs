using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _visual;
    [SerializeField] private Vector3 _moveVector;



    private bool _isManageToMove = false;
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void StopMove()
    {
        _isManageToMove = false;
    }
    public void StartMove()
    {
        _isManageToMove = true;
    }

    
    private void Update()
    {
        if (_isManageToMove == false)
        {
            return;
        }

        MoveForward();
    }
    private void MoveForward()
    {
        _visual.position += _moveVector * _speed * Time.deltaTime;
    }
}
