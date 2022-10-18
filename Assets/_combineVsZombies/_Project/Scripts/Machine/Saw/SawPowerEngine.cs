using System.Collections;
using UnityEngine;

public class SawPowerEngine : MonoBehaviour
{
    [SerializeField] private Improvement _improvement;


    private int _currentMassInSaw = 0;
    public void AddZombieToCut(Zombie zombie)
    {
        _currentMassInSaw += zombie.GetMassOfZombie();

        if (_currentMassInSaw >= _improvement.CurrentSawPower)
        {
            Debug.Log("<color=red>Overload</color>");
            Application.Quit();
        }
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
               
                _currentMassInSaw -= _improvement.CurrentCoolRate;
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
