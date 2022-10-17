using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private CollisionTrigered _collisionTrigered;



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
            print(zombie.Zombie.GetMassOfZombie() + " is mass of zombie");
        }
    }
}
