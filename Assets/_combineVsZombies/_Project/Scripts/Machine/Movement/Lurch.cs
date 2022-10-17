using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lurch : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private SideMovement _sideMovement;
    [SerializeField] private Clamps _clamps;
    [SerializeField] private float _lurchSpeed;
    [SerializeField] private float _lurchCoeff;

    private float _lurchIndex;
    private bool _isTurning;


    private void OnEnable()
    {
        _sideMovement.moved += Lurching;
    }
    private void OnDisable()
    {
        _sideMovement.moved -= Lurching;
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


    private void Update()
    {
        Turning();
    }
    private void Turning()
    {
        ClampRotation();
        print(_lurchIndex);
        _visual.rotation =  Quaternion.Lerp(_visual.rotation, Quaternion.Euler(0, _lurchIndex , 0), _lurchSpeed * Time.deltaTime);
    }
    private void ClampRotation()
    {
        _lurchIndex *= _lurchCoeff;
        _lurchIndex = Mathf.Clamp(_lurchIndex, _clamps.min, _clamps.max);
    }
}
