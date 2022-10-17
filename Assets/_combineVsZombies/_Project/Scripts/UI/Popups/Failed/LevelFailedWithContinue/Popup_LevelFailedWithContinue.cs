using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup_LevelFailedWithContinue : PopupBase
{
    [SerializeField, ReadOnly] private Button m_ButtonContinue;
    [SerializeField, ReadOnly] private Button m_ButtonNoThanks;
    [SerializeField, ReadOnly] private LevelFailedTimer m_LevelFailedTimer;

    private Tween m_PunchTween;

    protected override void setRefs()
    {
        base.setRefs();
        m_ButtonContinue = transform.FindDeepChild<Button>("Button_Continue");
        m_ButtonNoThanks = transform.FindDeepChild<Button>("Button_NoThanks");
        m_LevelFailedTimer = GetComponentInChildren<LevelFailedTimer>();
    }

    #region Init
    private void Awake()
    {
        m_ButtonContinue.Set(continueLevel);
        m_ButtonNoThanks.Set(chosedToFail);
    }

    private void OnEnable()
    {
        setInteractable(false);

        m_LevelFailedTimer.onTimeRunOut += chosedToFail;
    }

    private void OnDisable()
    {
        m_LevelFailedTimer.onTimeRunOut -= chosedToFail;
    }
    #endregion

    #region Callbacks
    private void continueLevel()
    {
        //TODO

        m_GameManager.LevelContinued();
        Close();
    }

    private void chosedToFail()
    {
        m_LevelFailedTimer.StopTimer();
        Close(true);
        PopupManager.Instance.Open(typeof(Popup_LevelFailed));
    }
    #endregion

    protected override void onEndOpeningAnimation()
    {
        base.onEndOpeningAnimation();
        setInteractable(true);
        m_LevelFailedTimer.StartTimer();
    }

    public override void Close(bool closeInstantly = false)
    {
        base.Close(closeInstantly);
        setInteractable(false);
    }

    #region Specific
    private void setInteractable(bool value)
    {
        m_ButtonContinue.interactable = value;
        m_ButtonNoThanks.interactable = value;

        resetTween();
        if (value)
        {
            m_PunchTween = m_ButtonContinue.transform.DOPunchScale(Vector3.one * 0.025f, 1f, 1).SetLoops(-1);
        }
    }

    private void resetTween()
    {
        m_PunchTween?.Kill();
        m_ButtonContinue.transform.localScale = Vector3.one;
    }
    #endregion
}
