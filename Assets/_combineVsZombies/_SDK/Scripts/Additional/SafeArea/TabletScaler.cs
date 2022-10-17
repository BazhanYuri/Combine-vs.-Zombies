using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class TabletScaler : MonoBehaviour
{
    [SerializeField, ReadOnly] private CanvasScaler m_CanvasScaler;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_CanvasScaler = GetComponent<CanvasScaler>();
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion;

    private void Awake()
    {
        checkResolution();
    }

    private void checkResolution()
    {
        m_CanvasScaler = GetComponent<CanvasScaler>();
        int divider = GCD(Screen.height, Screen.width);

        if (Screen.height / divider - Screen.width / divider < 2 && Screen.height / Screen.width != 2)
        {
            m_CanvasScaler.matchWidthOrHeight = 1f;
            return;
        }

        if(Screen.height / divider - Screen.width / divider == 2)
        {
            m_CanvasScaler.matchWidthOrHeight = 0.5f;
        }
    }

    private int GCD(int num1, int num2)
    {
        int Remainder;

        while (num2 != 0)
        {
            Remainder = num1 % num2;
            num1 = num2;
            num2 = Remainder;
        }

        return num1;
    }
}
