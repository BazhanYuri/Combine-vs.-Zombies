using UnityEngine;

[CreateAssetMenu(fileName = "ResultsInfo", menuName = "ScriptableObjects/LevelResultsInfo", order = 1)]
public class LevelPassedInfo : ScriptableObject
{
    public int passedDistance;
    public float costForDistance;



}
