using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Popup_LevelFailed : PopupBaseWithCollectable
{
    [SerializeField, ReadOnly] private Button m_TapToRestart;
    [SerializeField, ReadOnly] private RectTransform m_ButtonRect;

    private Tween m_PunchTween;

    #region Editor
    protected override void setRefs()
    {
        base.setRefs();
        m_TapToRestart = transform.FindDeepChild<Button>("Button_TapToRestart");
        m_ButtonRect = m_TapToRestart.GetComponent<RectTransform>();
    }
    #endregion

    #region Init
    private void Awake()
    {
        m_TapToRestart.Set(onTap);
    }

    protected override void OnEnable()
    {
        m_RewardAmount = 10;
        base.OnEnable();
    }

    private void OnDisable()
    {
        resetTween();
    }
    #endregion

    #region Callbacks
    private void onTap()
    {
        m_TapToRestart.interactable = false;
        Wallet.AddWithAnimation(m_RewardAmount, m_StartSendingPoint, m_GameManager.LoadLevel);
    }
    #endregion

    protected override void onEndOpeningAnimation()
    {
        base.onEndOpeningAnimation();

        m_TapToRestart.interactable = true;
        resetTween();
        m_PunchTween = m_ButtonRect.DOPunchScale(Vector3.one * 0.05f, 1.5f, 2).SetLoops(-1);
    }

    private void resetTween()
    {
        m_PunchTween?.Kill();
        m_ButtonRect.localScale = Vector3.one;
    }
}
