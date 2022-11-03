using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;

public class CollectableWallet : MonoBehaviour
{
    [field: SerializeField, PropertyOrder(-2)] public eCollectable CollectableType { get; private set; }
    [SerializeField, PropertyOrder(-2)] private HidingData m_HidingData;
    [Title("Visual")]
    [SerializeField, ReadOnly] private TextMeshProUGUI m_AmountDisplay;
    [SerializeField, ReadOnly] private RectTransform m_AmountRect;
    [SerializeField, ReadOnly] private RectTransform m_TargetTransform;
    [SerializeField, ReadOnly] private CollectableSender m_CollectableSender;
    [SerializeField, ReadOnly] private GameObject m_Visual;

    public event Action<int> onValueChanged;

    private Tween m_PunchingAmount, m_PunchingIcon;
    private Sequence m_ShakeAmount;
    private Vector3 m_StartAmountPosition;

    #region Amount
    [ShowInInspector, PropertyOrder(-1)]
    public int Amount
    {
        get
        {
            return 100000;
            return m_StorageManager.GetCollectable(CollectableType);
        }

        private set
        {
            onValueChanged?.Invoke(value);
            m_StorageManager.SetCollectable(CollectableType, value);
        }
    }
    #endregion

    #region Editor
    [Button]
    private void setRefs()
    {
        m_AmountDisplay = GetComponentInChildren<TextMeshProUGUI>(true);
        m_AmountRect = m_AmountDisplay.GetComponent<RectTransform>();
        m_TargetTransform = transform.FindDeepChild<RectTransform>("Icon");
        m_CollectableSender = GetComponentInChildren<CollectableSender>();
        m_Visual = transform.FindDeepChild<GameObject>("Visual");
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    private void Awake()
    {
        updateAmount(Amount);
        m_CollectableSender.Initialize(this);

        m_StartAmountPosition = m_AmountRect.anchoredPosition;
    }

    private void OnEnable()
    {
        onValueChanged += updateAmount;

        if(m_HidingData.IsNeedHideOnLevelCompleted)
            GameManager.onLevelCompleted += hide;

        if (m_HidingData.IsNeedHideOnLevelFailed)
        {
            GameManager.onLevelFailed += show;
            GameManager.onLevelContinued += show;
        }

        GameManager.onGameReset += show;
    }

    private void OnDisable()
    {
        onValueChanged -= updateAmount;

        if (m_HidingData.IsNeedHideOnLevelCompleted)
            GameManager.onLevelCompleted -= hide;

        if (m_HidingData.IsNeedHideOnLevelFailed)
        {
            GameManager.onLevelFailed -= show;
            GameManager.onLevelContinued -= show;
        }

        GameManager.onGameReset -= show;
    }
    #endregion

    #region Callbacks
    private void updateAmount(int amount)
    {
        m_AmountDisplay.text = NumberFormatter.GetFormatedNumber(amount);
    }

    private void hide()
    {
        m_Visual.SetActive(false);
    }

    private void show()
    {
        if (m_Visual.activeInHierarchy)
            return;

        m_Visual.SetActive(true);
    }
    #endregion

    public void Add(int amount, bool isNeededAnimation = true)
    {
        playAmountAnimation();

        if (isNeededAnimation)
        {
            m_PunchingIcon?.Kill();
            m_TargetTransform.localScale = Vector3.one;
            m_PunchingIcon = m_TargetTransform.DOPunchScale(m_WalletData.IconAnimation.Scale,
                m_WalletData.IconAnimation.Time,
                m_WalletData.IconAnimation.Vibration);
        }

        Amount += amount;
    }

    public void AddWithAnimation(int amount, RectTransform start, Action onEnd = null)
    {
        if(m_CollectableSender != null)
        {
            m_CollectableSender.Send(start, amount, m_TargetTransform, onEnd);
        }
        else
        {
            Debug.LogError("Collectable sender wasn't setup!");
        }
    }

    public bool CanRemove(int amount)
    {
        int remainingAmount = Amount - amount;
        if (remainingAmount >= 0)
        {
            playAmountAnimation();
            Amount = remainingAmount;
        }
        else
        {
            m_ShakeAmount?.Kill();
            m_AmountRect.anchoredPosition = m_StartAmountPosition;

            m_ShakeAmount = DOTween.Sequence()
                .Append(m_AmountRect.DOPunchAnchorPos(Vector2.right * 5, 0.1f, 2))
                .Append(m_AmountRect.DOPunchAnchorPos(Vector2.left * 5, 0.1f, 2));
        }

        return remainingAmount >= 0;
    }

    #region Specific
    private void playAmountAnimation()
    {
        m_PunchingAmount?.Kill();
        m_AmountRect.localScale = Vector3.one;
        m_PunchingAmount = m_AmountRect.DOPunchScale(m_WalletData.AmountAnimation.Scale,
            m_WalletData.AmountAnimation.Time,
            m_WalletData.AmountAnimation.Vibration);
    }
    #endregion

    private StorageManager m_StorageManager => StorageManager.Instance;
    private CollectableAnimationData.WalletData m_WalletData
        => GameConfig.Instance.CollectableData.CollectableAnimationData.WalletAnimationData;

    [Serializable]
    public class HidingData
    {
        [field: SerializeField] public bool IsNeedHideOnLevelCompleted { get; private set; }
        [field: SerializeField] public bool IsNeedHideOnLevelFailed { get; private set; } = true;
    }
}
