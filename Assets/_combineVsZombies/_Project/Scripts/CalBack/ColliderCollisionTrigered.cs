using System;
using UnityEngine;


public class ColliderCollisionTrigered : MonoBehaviour
{
    public event Action<Collider> Trigered;




    private void OnTriggerEnter(Collider other)
    {
        Trigered?.Invoke(other);
    }
}
