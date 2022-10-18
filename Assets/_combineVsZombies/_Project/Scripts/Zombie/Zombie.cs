using System.Collections;
using UnityEngine;



public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private Transform[] _zombieTypes;
    [SerializeField] private ZombieInfoManager _zombieInfoManager;

    private int _zombieTypeIndex = 0;


    
    public void SetZombieType(int index)
    {
        HideAllZombies();
        _zombieTypeIndex = index;
        _zombieTypes[_zombieTypeIndex].gameObject.SetActive(true);
    }
    public int GetMassOfZombie()
    {
        return _zombieInfoManager.Masses[_zombieTypeIndex];
    }
    public void DisactivateZombie(float time)
    {
        StartCoroutine(DisactivatingZombieWithTime(time));
    }
    private IEnumerator DisactivatingZombieWithTime(float time)
    {
        yield return new WaitForSeconds(time);
        _visual.parent.gameObject.SetActive(false);
    }
    private void HideAllZombies()
    {
        for (int i = 0; i < _zombieTypes.Length; i++)
        {
            _zombieTypes[i].gameObject.SetActive(false);
        }
    }
    public void MoveZombieTo(Transform obj)
    {
        _forwardMovement.StopMove();
        MoveVisualToSaw(obj);
    }
    private void MoveVisualToSaw(Transform obj)
    {
        Transform zombieToor = _visual.parent;
        zombieToor.parent = obj;
    }
}
