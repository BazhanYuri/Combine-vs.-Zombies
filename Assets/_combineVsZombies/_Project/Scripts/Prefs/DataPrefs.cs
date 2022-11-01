using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPrefs : MonoBehaviour
{
    private const string _engineLevel = "engineLevel";
    private const string _sawLevel = "sawLevel";
    private const string _coolrateLevel = "coolRateLevel";
    private const string _distanceRecord = "distanceRecord";




    public static void SaveImprovenes(ImproveType improveType, int value)
    {
        switch (improveType)
        {
            case ImproveType.Engine:
                PlayerPrefs.SetInt(_engineLevel, value);
                break;
            case ImproveType.Saw:
                PlayerPrefs.SetInt(_sawLevel, value);
                break;
            case ImproveType.Cooldown:
                PlayerPrefs.SetInt(_coolrateLevel, value);
                break;
            default:
                break;
        }
    }
    public static int GetImprovenes(ImproveType improveType)
    {
        switch (improveType)
        {
            case ImproveType.Engine:
                return PlayerPrefs.GetInt(_engineLevel);
            case ImproveType.Saw:
                return PlayerPrefs.GetInt(_sawLevel);
            case ImproveType.Cooldown:
                return PlayerPrefs.GetInt(_coolrateLevel);
            default:
                throw new System.Exception();
        }
    }
    public static void SaveDistanceRecord(int value)
    {
        PlayerPrefs.SetInt(_distanceRecord, value);
    }
    public static int GetDistanceRecord()
    {
        return PlayerPrefs.GetInt(_distanceRecord);
    }
}
