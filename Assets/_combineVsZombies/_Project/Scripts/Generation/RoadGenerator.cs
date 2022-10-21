using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Road _firstRoad;
    [SerializeField] private Road _secondRoad;




    private void Start()
    {
        _firstRoad.MoveRoadTo(_secondRoad);
        _secondRoad.MoveRoadTo(_firstRoad);
    }


}
