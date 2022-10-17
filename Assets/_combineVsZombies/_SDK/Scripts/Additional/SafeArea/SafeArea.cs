using Sirenix.OdinInspector;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField, ReadOnly] private RectTransform m_RectTransform;

    #region Editor
    [Button]
    public void SetRefs()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        SetRefs();
    }
    #endregion

    private void Awake()
    {
        updateCanvasRect();
    }

    private void updateCanvasRect()
    {
        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        m_RectTransform.anchorMin = anchorMin;
        m_RectTransform.anchorMax = anchorMax;
    }
}