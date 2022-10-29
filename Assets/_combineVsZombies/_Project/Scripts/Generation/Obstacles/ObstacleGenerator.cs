using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Machine _machine;
    [SerializeField] private ObstaclesPool _obstaclesPool;
    [SerializeField] private PassedDistanceManager _passedDistanceManager;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Clamps _spawnXClamps;
    [SerializeField] private float _timeToSpawnObstacle;




    private void OnEnable()
    {
        _machine.Death.onDead += StopSpawning;
        _machine.GameStarter.GameStarted += StartSpawn;
    }
    private void OnDisable()
    {
        _machine.Death.onDead -= StopSpawning;
        _machine.GameStarter.GameStarted += StartSpawn;

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
            yield return new WaitForSeconds(_timeToSpawnObstacle);
            SpawnOneZombie();
        }
    }
    private void SpawnOneZombie()
    {
        Obstacle obstacle = _obstaclesPool.Take();
        obstacle.transform.position = _spawnPoint.position;
        obstacle.transform.position = new Vector3(Random.Range(_spawnXClamps.min, _spawnXClamps.max), obstacle.transform.position.y, obstacle.transform.position.z);
        obstacle.transform.rotation = Quaternion.identity;
    }
}
