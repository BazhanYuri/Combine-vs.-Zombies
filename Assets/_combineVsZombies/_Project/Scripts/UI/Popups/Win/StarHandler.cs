using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StarHandler : MonoBehaviour
{
    [SerializeField, ReadOnly] private Image[] m_Stars;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Stars = transform.GetComponentsInChildren<Image>();
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    public void ShowCompletedStars(int amount = 3)
    {
        for(int i = 0; i < m_Stars.Length; i++)
        {
            m_Stars[i].sprite = i < amount ? m_WinPopupData.FullStar : m_WinPopupData.EmptySprite; 
        }
    }

    private PopupVariablesEditor.WinPopupData m_WinPopupData => GameConfig.Instance.Popup.WinPopup;
}
