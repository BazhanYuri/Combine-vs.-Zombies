using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private ForwardMovement _forwardMovement;
    [SerializeField] private ZombieInfoConteiner _zombieInfoConteiner;
    [SerializeField] private ZombieInfoConteiner[] _zombieTypes;



    private int _zombieTypeIndex = 0;


    public int GetMassOfZombie()
    {
        return _zombieTypes[_zombieTypeIndex].Mass;
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
