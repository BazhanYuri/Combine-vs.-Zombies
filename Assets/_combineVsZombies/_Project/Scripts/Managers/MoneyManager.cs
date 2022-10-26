using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextUpdatedTrigger _textUpdaterDistanceCoins;
    [SerializeField] private TextUpdatedTrigger _textUpdaterKilledZombieCount;
    [SerializeField] private TextUpdatedTrigger _textUpdaterKilledZombieMass;
    [SerializeField] private float costForDistance;
    [SerializeField] private float costForOneZombie;
    [SerializeField] private float costForOneKG;


    private int _passedDistance;
    private int _killedZombies;
    private int _killedZombiesMass;
    private int _moneyCount = 0;

    public int PassedDistance 
    {

        set { _passedDistance = value; AddCoinsForDistance(); }
    }

    public int KilledZombies 
    { 
        set { _killedZombies = value; AddCoinsForKilledZombies(); } 
    }

    public int KilledZombiesMass
    {
        set { _killedZombiesMass = value; AddCoinsForMass(); }
    }

    private void AddCoinsForDistance()
    {
        _textUpdaterDistanceCoins.InvokeUpdated((int)(_passedDistance * costForDistance));
    }
    private void AddCoinsForKilledZombies()
    {
        _textUpdaterKilledZombieCount.InvokeUpdated((int)(_killedZombies * costForOneZombie));
    }
    private void AddCoinsForMass()
    {
        _textUpdaterKilledZombieMass.InvokeUpdated((int)(_killedZombiesMass * costForOneKG));
    }
}
