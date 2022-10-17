using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Popup_LevelStart : PopupBase
{
    [SerializeField, ReadOnly] private Button m_TapToStart;
    [SerializeField, ReadOnly] private RectTransform m_ButtonRect;

    private Tween m_PunchingTween;

    protected override void setRefs()
    {
        base.setRefs();
        m_TapToStart = transform.FindDeepChild<Button>("TapToStart");
        m_ButtonRect = m_TapToStart.GetComponent<RectTransform>();
    }

    #region Init
    private void Awake()
    {
        m_TapToStart.Set(onTap);
    }

    private void OnDisable()
    {
        resetButtonAnimation();
    }
    #endregion

    public override void Open()
    {
        if (gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);

        m_CanvasGroup.alpha = 1;
        m_CanvasGroup.interactable = true;

        m_PunchingTween?.Kill();
        m_PunchingTween = m_ButtonRect.DOPunchScale(Vector3.one * 0.05f, 1.5f, 1).SetLoops(-1);
    }

    protected override void onEndClosingAnimation()
    {
        base.onEndClosingAnimation();
        m_GameManager.StartLevel();
    }

    private void onTap()
    {
        resetButtonAnimation();
        Close();
    }

    private void resetButtonAnimation()
    {
        m_PunchingTween?.Kill();
        m_ButtonRect.localScale = Vector3.one;
    }
}
