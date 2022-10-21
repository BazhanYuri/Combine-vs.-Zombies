using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private CollisionTrigered _collisionTrigered;
    [SerializeField] private Transform _roadEndPoint;

    private Road _roadNext;


    private void OnEnable()
    {
        _collisionTrigered.Trigered += MoveToNextRoad;
    }
    private void OnDisable()
    {
        _collisionTrigered.Trigered -= MoveToNextRoad;
    }
    public void MoveRoadTo(Road road)
    {
        _roadNext = road;
    }
    public void MoveToNextRoad()
    {
        _roadNext.transform.position = _roadEndPoint.position;
    }
}
