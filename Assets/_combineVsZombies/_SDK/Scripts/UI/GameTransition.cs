using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class GameTransition : Singleton<GameTransition>
{
    [SerializeField, ReadOnly] private Image m_Background;

    #region Editor
    private void setRefs()
    {
        m_Background = GetComponentInChildren<Image>(true);
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    public void DoStartTransition()
    {
        m_Background.gameObject.SetActive(true);
        m_Background.SetAlpha(1);
        fadeOut();
    }

    public void DoTransition(Action onEnd)
    {
        m_Background.gameObject.SetActive(true);
        m_Background.SetAlpha(0);
        m_Background.DOFade(1f, 0.35f).OnComplete(() => fadeOut(onEnd));
    }

    private void fadeOut(Action onEnd)
    {
        onEnd?.Invoke();
        fadeOut();
    }

    private void fadeOut()
    {
        m_Background.DOFade(0, 0.35f).OnComplete(() => m_Background.gameObject.SetActive(false));
    }
}
