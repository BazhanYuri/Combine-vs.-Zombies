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
    [SerializeField] private float _improveSpeedIndex;
    [SerializeField] private float _improveSawIndex;
    [SerializeField] private float _improveCoolRateIndex;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button _speedImproveButton;
    [SerializeField] private Button _sawImproveButton;
    [SerializeField] private Button _coolRateImproveButton;

    private int _impSpeedStep = 0;
    private int _impSawStep = 0;
    private int _impCoolRateStep = 0;


    private void OnEnable()
    {
        _speedImproveButton.onClick.AddListener(ImproveSpeed);
        _sawImproveButton.onClick.AddListener(ImproveSaw);
        _coolRateImproveButton.onClick.AddListener(ImproveCoolRate);
    }
    private void OnDisable()
    {
        _speedImproveButton.onClick.RemoveAllListeners();
    }
    private void ImproveSpeed()
    {
        _impSpeedStep++;
    }
    private void ImproveSaw()
    {
        _impSawStep++;
    }
    private void ImproveCoolRate()
    {
        _impCoolRateStep++;
    }
    public float CalculateValue(ImproveType improveType, float standartValue)
    {
        return standartValue * CalculateIndex(improveType);
    }
    private float CalculateIndex(ImproveType improveType)
    {
        switch (improveType)
        {
            case ImproveType.Engine:
                return CalculateIndex(_improveSpeedIndex, _impSpeedStep);
            case ImproveType.Saw:
                return CalculateIndex(_improveSawIndex, _impSawStep);
            case ImproveType.Cooldown:
                return CalculateIndex(_improveCoolRateIndex, _impCoolRateStep);
            default:
                return 1;
        }
    }
    private float CalculateIndex(float startImproveIndex, int index)
    {
        float endValue = startImproveIndex;

        if (index == 0)
        {
            endValue = 1;
        }
        else
        {
            for (int i = 0; i < index; i++)
            {
                endValue *= startImproveIndex;
            }
        }

        return endValue;
    }
}
