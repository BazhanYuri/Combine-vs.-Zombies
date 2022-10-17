using Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class PopupBaseWithCollectable : PopupBase
{
    [SerializeField, PropertyOrder(-1)] private eCollectable m_CollectableType;
    [Title("Collectable")]
    [SerializeField, ReadOnly] protected TextMeshProUGUI m_CollectableAmountDisplay;
    [SerializeField, ReadOnly] protected RectTransform m_StartSendingPoint;

    protected int m_RewardAmount = 250; // change

    #region Editor
    protected override void setRefs()
    {
        base.setRefs();

        m_StartSendingPoint = transform.FindDeepChild<RectTransform>("CoinIcon");
        m_CollectableAmountDisplay = transform.FindDeepChild<TextMeshProUGUI>("Text_Value");
    }
    #endregion

    #region Init
    protected virtual void OnEnable()
    {
        m_CollectableAmountDisplay.text = $"+ {m_RewardAmount}";
    }
    #endregion

    #region Wallet
    private CollectableWallet m_Wallet;
    protected CollectableWallet Wallet
    {
        get
        {
            if (m_Wallet == null)
            {
                m_Wallet = CollectableManager.Instance.GetWallet(m_CollectableType);
            }

            return m_Wallet;
        }
    }
    #endregion
}
