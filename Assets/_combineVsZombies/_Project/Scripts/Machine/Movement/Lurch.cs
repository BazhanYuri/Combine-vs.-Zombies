using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lurch : MonoBehaviour
{
    [SerializeField] private Machine _machine;
    [SerializeField] private Transform _visual;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private Clamps _clamps;
    [SerializeField] private float _lurchSpeed;
    [SerializeField] private float _lurchCoeff;

    private float _lurchIndex;
    private bool _isTurning;
    private bool _isCanTurn = true;


    private void OnEnable()
    {
        _sideMovement.moved += Lurching;
        _machine.Death.onDead += StopTurning;
    }
    private void OnDisable()
    {
        _sideMovement.moved -= Lurching;
        _machine.Death.onDead -= StopTurning;
    }

    private void Lurching(float velocity)
    {
        if (velocity != 0)
        {
            _lurchIndex += velocity;
            _isTurning = true;
            return;
        }
        _lurchIndex = 0;
    }
    private void StopTurning()
    {
        _isCanTurn = false;
    }

    private void Update()
    {
        if (_isCanTurn == true)
        {
            Turning();
        }
    }
    private void Turning()
    {
        ClampRotation();
        _visual.rotation =  Quaternion.Lerp(_visual.rotation, Quaternion.Euler(0, _lurchIndex , 0), _lurchSpeed * Time.deltaTime);
    }
    private void ClampRotation()
    {
        _lurchIndex *= _lurchCoeff;
        _lurchIndex = Mathf.Clamp(_lurchIndex, _clamps.min, _clamps.max);
    }
}
