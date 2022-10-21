using System.Collections;
using UnityEngine;

public class SawPowerEngine : MonoBehaviour
{
    [SerializeField] private TextUpdatedTrigger _textUpdatedTriggerWeight;
    [SerializeField] private TextUpdatedTrigger _textUpdatedTriggerWeightPercent;
    [SerializeField] private TextUpdatedTrigger _textUpdatedTriggerTotalWeight;
    [SerializeField] private TextUpdatedTrigger _textUpdatedTriggerZombieCount;

    [SerializeField] private Death _death;
    [SerializeField] private Improvement _improvement;


    private float _currentMassInSaw = 0;
    private int _zombieCount = 0;
    private int _totalZombieMass = 0;
    


    public int PercentageOfSawWeightFull
    {
        get;
        set;
    }
    private void OnEnable()
    {
        _death.onDead += StopSaw;
    }
    private void OnDisable()
    {
        _death.onDead -= StopSaw;
    }


    public void AddZombieToCut(Zombie zombie)
    {
        _currentMassInSaw += zombie.GetMassOfZombie();
        _totalZombieMass += zombie.GetMassOfZombie();
        _zombieCount++;
        
        
        _textUpdatedTriggerZombieCount.InvokeUpdated(_zombieCount);
        _textUpdatedTriggerTotalWeight.InvokeUpdated(_totalZombieMass);
    }



    private void Start()
    {
        StartCoroutine(Grinding());
    }
    private IEnumerator Grinding()
    {
        while (true)
        {
            if (_currentMassInSaw > 0)
            {
                _currentMassInSaw -= (_improvement.CurrentCoolRate / 10);
            }
            _textUpdatedTriggerWeight.InvokeUpdated((int)_currentMassInSaw);
            _textUpdatedTriggerWeightPercent.InvokeUpdated(CalculatePerentageOfOverriding());
            if (_currentMassInSaw >= _improvement.CurrentSawPower)
            {
                _death.Dead();
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    private int CalculatePerentageOfOverriding()
    {
        PercentageOfSawWeightFull = (int)((_currentMassInSaw / (float)_improvement.CurrentSawPower) * 100);
        return PercentageOfSawWeightFull;
    }
    private void StopSaw()
    {
        StopAllCoroutines();
    }
}
