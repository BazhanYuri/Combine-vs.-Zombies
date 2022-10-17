using Extensions;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField, ReadOnly] protected CanvasGroup m_CanvasGroup;

    #region Editor
    [Button]
    protected virtual void setRefs()
    {
        m_CanvasGroup = transform.FindDeepChild<CanvasGroup>("Group");
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    public virtual void Open()
    {
        if (gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);

        m_CanvasGroup.alpha = 0;
        m_CanvasGroup.interactable = true;

        m_CanvasGroup.DOFade(1, m_PopupData.m_FadeInTime).OnComplete(onEndOpeningAnimation);
    }

    protected virtual void onEndOpeningAnimation() { }

    public virtual void Close(bool closeInstantly = false)
    {
        
        if (gameObject.activeInHierarchy == false)
            return;

        if (closeInstantly)
        {
            m_CanvasGroup.alpha = 0;
            m_CanvasGroup.interactable = false;
            gameObject.SetActive(false);
            onEndClosingAnimation();
        }
        else
        {
            m_CanvasGroup.DOFade(0, m_PopupData.m_FadeOutTime).OnComplete(() =>
            {
                m_CanvasGroup.interactable = false;
                gameObject.SetActive(false);
                onEndClosingAnimation();
            });
        }
    }

    protected virtual void onEndClosingAnimation() { }

    private PopupVariablesEditor m_PopupData => GameConfig.Instance.Popup;
    protected GameManager m_GameManager => GameManager.Instance;
}
