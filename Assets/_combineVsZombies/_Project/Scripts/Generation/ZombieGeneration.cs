using System.Collections;
using UnityEngine;

public class ZombieGeneration : MonoBehaviour
{
    [SerializeField] private Machine _machine;
    [SerializeField] private PassedDistanceManager _passedDistanceManager;
    [SerializeField] private ZombieInfoManager _zombieInfoManager;
    [SerializeField] private ZombiePool _zombiePool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Clamps _spawnXClamps;
    [SerializeField] private float _timeToSpawnZombies;

    [Space]
    [SerializeField] private int _startPoolMass;
    [SerializeField] private float _massForOneMeter;



    public void ConnectSpawnSpeedWithVechicleSpeed(float percantageOfImprovenesSpeed)
    {
        _timeToSpawnZombies /= percantageOfImprovenesSpeed;
    }
    private void OnEnable()
    {
        _machine.Death.onDead += StopSpawning;
        _machine.GameStarter.GameStarted += StartSpawn;
    }
    private void OnDisable()
    {
        _machine.Death.onDead -= StopSpawning;
        _machine.GameStarter.GameStarted -= StartSpawn;

    }
    private void StopSpawning()
    {
        StopAllCoroutines();
    }
    private void StartSpawn()
    {
        StartCoroutine(SpawnZombie());
    }
    
    private IEnumerator SpawnZombie()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawnZombies);
            StartCoroutine(SpawnPoolOfZombie());
        }
    }
    private IEnumerator SpawnPoolOfZombie()
    {
        bool massEnough = true;
        int currentMass = CalculatePoolMass();

        while (massEnough)
        {
            yield return new WaitForSeconds(0.2f);
            int zombieIndex = GetRandomZombie();

            while (ChoosedZombieIsForDistance(zombieIndex) == false)
            {
                zombieIndex = GetRandomZombie();
            }
            currentMass -= _zombieInfoManager.Masses[zombieIndex];
            SpawnOneZombie(zombieIndex);

            if (currentMass < _zombieInfoManager.Masses[0])
            {
                massEnough = false;
            }
        }
    }
    private void SpawnOneZombie(int zombieIndex)
    {
        Zombie zombie = _zombiePool.Take();
        zombie.SetZombieType(zombieIndex);
        zombie.transform.position = _spawnPoint.position;
        zombie.transform.position = new Vector3(Random.Range(_spawnXClamps.min, _spawnXClamps.max), zombie.transform.position.y, zombie.transform.position.z);
        zombie.transform.rotation = Quaternion.identity;
    }

    private int GetRandomZombie()
    {
        return Random.Range(0, 3);
    }
    private bool ChoosedZombieIsForDistance(int zombieType)
    {
        if (_passedDistanceManager.PassedDistance >= _zombieInfoManager.Distances[zombieType])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private int CalculatePoolMass()
    {
        return  _startPoolMass + (int)(_passedDistanceManager.PassedDistance * _massForOneMeter);
    }
}
