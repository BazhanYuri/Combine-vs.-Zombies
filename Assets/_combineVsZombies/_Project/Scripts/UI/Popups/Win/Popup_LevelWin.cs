using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup_LevelWin : PopupBaseWithCollectable
{
    [Title("Button")]
    [SerializeField, ReadOnly] private Button m_ButtonNextLevel;
    [SerializeField, ReadOnly] private Button m_ButtonClaimX2;
    
    [Title("Stars")]
    [SerializeField, ReadOnly] private StarHandler m_StarHandler;

    

    protected override void setRefs()
    {
        base.setRefs();
        m_ButtonNextLevel = transform.FindDeepChild<Button>("Button_Next");
        m_ButtonClaimX2 = transform.FindDeepChild<Button>("Button_x2Claim");

        m_StarHandler = GetComponentInChildren<StarHandler>();
    }

    #region Init
    private void Awake()
    {
        m_ButtonNextLevel.Set(nextLevel);
        m_ButtonClaimX2.Set(claimX2);
    }

    protected override void OnEnable()
    {
        m_RewardAmount = 275;
        base.OnEnable();
    }
    #endregion

    public override void Open()
    {
        m_StarHandler.ShowCompletedStars(); // set stars amount
        base.Open();
    }

    #region ButtonCallbacks
    private void nextLevel()
    {
        sendReward();
    }

    private void claimX2()
    {
        m_CollectableAmountDisplay.text = $"+{m_RewardAmount * 2}";
        sendReward(2);
    }
    #endregion

    protected override void onEndOpeningAnimation()
    {
        base.onEndOpeningAnimation();
        setInteractable(true);
    }

    #region Specific
    private void sendReward(int modifier = 1)
    {
        setInteractable(false);
        Wallet.AddWithAnimation(m_RewardAmount * modifier, m_StartSendingPoint, m_GameManager.LoadLevel);
    }

    private void setInteractable(bool value)
    {
        m_ButtonNextLevel.interactable = value;
        m_ButtonClaimX2.interactable = value;
    }
    #endregion
}
