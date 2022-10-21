using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private SawPowerEngine _sawPowerEngine;
    [SerializeField] private ColliderCollisionTrigered _collisionTrigered;




    private void OnEnable()
    {
        _collisionTrigered.Trigered += CheckCollider;
    }
    private void OnDisable()
    {
        _collisionTrigered.Trigered -= CheckCollider;
    }



    
    private void CheckCollider(Collider collider)
    {
        if (collider.TryGetComponent(out ZombieTrigger zombie))
        {
            zombie.Zombie.MoveZombieTo(_collisionTrigered.transform);
            zombie.Zombie.DisactivateZombie(1);
            _sawPowerEngine.AddZombieToCut(zombie.Zombie);
        }
    }
}
