using System;
using UnityEngine;

[Serializable]
public class Clamps
{
    public float min;
    public float max;
}

[RequireComponent(typeof(ScreenTapHandler))]
public class SideMovement : MonoBehaviour
{
    public event Action<float> moved;

    [SerializeField] private Machine _machine;
    [SerializeField] private Clamps _clamps;
    [SerializeField] private float _speed;
    [SerializeField] private float _tapSpeedIndex;
    [SerializeField] private Transform _visual;

    [SerializeField] private ScreenTapHandler _screenTapHandler;

    private float _pointToMove;


    public void StopMove()
    {
        _speed = 0;
    }

    private void OnEnable()
    {
        _screenTapHandler.ScreenTouchedCallback += MoveSides;
        _machine.Death.onDead += StopMove;
    }
    private void OnDisable()
    {
        _screenTapHandler.ScreenTouchedCallback -= MoveSides;
        _machine.Death.onDead -= StopMove;
    }
    private void Update()
    {
        MoveVisual();
    }
    private void MoveSides(SideTouched sideTouched)
    {
        if (sideTouched == SideTouched.Left)
        {
            _pointToMove -= _tapSpeedIndex * Time.deltaTime;
            moved?.Invoke(-_tapSpeedIndex * Time.deltaTime);
        }
        else if (sideTouched == SideTouched.Right)
        {
            _pointToMove += _tapSpeedIndex * Time.deltaTime;
            moved?.Invoke(_tapSpeedIndex * Time.deltaTime);
        }
        else
        {
            moved?.Invoke(0);
        }
        ClampXPosition();
    }
    private void MoveVisual()
    {
        _visual.localPosition = Vector3.MoveTowards(_visual.localPosition, new Vector3(_pointToMove, _visual.localPosition.y, _visual.localPosition.z), Time.deltaTime * _speed);
    }
    private void ClampXPosition()
    {
        _pointToMove = Mathf.Clamp(_pointToMove, _clamps.min, _clamps.max);
    }
}
