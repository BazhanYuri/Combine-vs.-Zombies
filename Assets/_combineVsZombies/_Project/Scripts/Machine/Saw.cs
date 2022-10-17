using UnityEngine;

public class Saw : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("trigeg");

        if (other.TryGetComponent(out Zombie zombie))
        {
            zombie.gameObject.SetActive(false);
            print("trigeg");

        }
    }
}
