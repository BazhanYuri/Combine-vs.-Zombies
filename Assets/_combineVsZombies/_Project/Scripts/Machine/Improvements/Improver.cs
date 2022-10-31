using UnityEngine;
using UnityEngine.UI;


public enum ImproveType
{
    Engine,
    Saw,
    Cooldown
}
public class Improver : MonoBehaviour
{
    [SerializeField] private ButtonImproveContainer _improveSpeedIndex;
    [SerializeField] private ButtonImproveContainer _improveSawIndex;
    [SerializeField] private ButtonImproveContainer _improveCoolRateIndex;
   



    
    public float CalculateValue(ImproveType improveType, float standartValue)
    {
        return standartValue * CalculateIndex(improveType);
    }
    private float CalculateIndex(ImproveType improveType)
    {
        switch (improveType)
        {
            case ImproveType.Engine:
                return CalculatingImprovements.CalculateIndex(_improveSpeedIndex.ImproveIndex, _improveSpeedIndex.IndexStep);
            case ImproveType.Saw:
                return CalculatingImprovements.CalculateIndex(_improveSawIndex.ImproveIndex, _improveSawIndex.IndexStep);
            case ImproveType.Cooldown:
                return CalculatingImprovements.CalculateIndex(_improveCoolRateIndex.ImproveIndex, _improveCoolRateIndex.IndexStep);
            default:
                return 1;
        }
    }
}
