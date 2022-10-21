using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPool : MonoBehaviour
{
    [SerializeField] private int _roadCount;
    [SerializeField] private Road _roadPrefab;
    [SerializeField] private Transform _roadRoot;


    private List<Road> _roadPool = new List<Road>();


    public Road Take()
    {
        for (int i = 0; i < _roadPool.Count; i++)
        {
            if (_roadPool[i].gameObject.active == false)
            {
                _roadPool[i].gameObject.SetActive(true);
                _roadPool[i].transform.parent = _roadRoot;
                return _roadPool[i];
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
        for (int i = 0; i < _roadCount; i++)
        {
            Road zombie = Instantiate(_roadPrefab);
            zombie.gameObject.SetActive(false);
            zombie.transform.parent = _roadRoot;
            _roadPool.Add(zombie);
        }
    }
}
