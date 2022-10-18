using UnityEngine;



[CreateAssetMenu(fileName = "ZombieInfo", menuName = "ScriptableObjects/ZombieInfo", order = 1)]

public class ZombieInfoManager : ScriptableObject
{
    [SerializeField] private int[] _masses;
    [SerializeField] private int[] _distances;

    public int[] Masses { get => _masses;}
    public int[] Distances { get => _distances; }
}
