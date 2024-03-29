using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisactivateZone : MonoBehaviour
{
    [SerializeField] private ColliderCollisionTrigered _zoneCollision;




    private void OnEnable()
    {
        _zoneCollision.Trigered += HideObject;
    }
    private void OnDisable()
    {
        _zoneCollision.Trigered -= HideObject;
    }
    private void HideObject(Collider obj)
    {
        if (obj.TryGetComponent(out ZombieTrigger zombie))
        {
            zombie.Zombie.gameObject.SetActive(false);
        }
        if (obj.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.gameObject.SetActive(false);
        }
    }
}
