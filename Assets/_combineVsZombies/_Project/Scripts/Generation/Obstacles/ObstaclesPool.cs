using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    [SerializeField] private int _obstacleCount;
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Transform _obstacleRoot;

    private List<Obstacle> _obstaclePool = new List<Obstacle>();




    public Obstacle Take()
    {
        for (int i = 0; i < _obstaclePool.Count; i++)
        {
            if (_obstaclePool[i].gameObject.active == false)
            {
                _obstaclePool[i].gameObject.SetActive(true);
                _obstaclePool[i].transform.parent = _obstacleRoot;
                return _obstaclePool[i];
            }
        }
        return null;
    }

    private void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        for (int i = 0; i < _obstacleCount; i++)
        {
            Obstacle zombie = Instantiate(_obstaclePrefab);
            zombie.gameObject.SetActive(false);
            zombie.transform.parent = _obstacleRoot;
            _obstaclePool.Add(zombie);
        }
    }
}
