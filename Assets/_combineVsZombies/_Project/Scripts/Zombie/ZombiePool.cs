using System.Collections.Generic;
using UnityEngine;


public class ZombiePool : MonoBehaviour
{
    [SerializeField] private int _zombieCount;
    [SerializeField] private Zombie _zombiePrefab;
    [SerializeField] private Transform _zomiesRoot;

    private List<Zombie> _zombiePool = new List<Zombie>();




    public Zombie Take()
    {
        for (int i = 0; i < _zombiePool.Count; i++)
        {
            if (_zombiePool[i].gameObject.active == false)
            {
                _zombiePool[i].
                _zombiePool[i].gameObject.SetActive(true);
                _zombiePool[i].transform.parent = _zomiesRoot;
                return _zombiePool[i];
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
        for (int i = 0; i < _zombieCount; i++)
        {
            Zombie zombie = Instantiate(_zombiePrefab);
            zombie.SetZombieType(0);
            zombie.gameObject.SetActive(false);
            zombie.transform.parent = _zomiesRoot;
            _zombiePool.Add(zombie);
        }
    }
}
