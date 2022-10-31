using UnityEngine;

public class CalculatingImprovements
{
    public static float CalculateIndex(float startImproveIndex, int step)
    {
        float endValue = startImproveIndex;

        if (step == 0)
        {
            endValue = 1;
        }
        else
        {
            for (int i = 1; i < step; i++)
            {
                endValue *= startImproveIndex;
            }
        }

        return endValue;
    }
}
