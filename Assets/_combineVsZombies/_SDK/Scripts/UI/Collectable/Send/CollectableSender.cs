using Extensions;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollectableSender : MonoBehaviour
{
    [SerializeField] private CollectableVisualPart m_CollectableVisualPartPrefab;
    [SerializeField] private int m_Amount;
    [SerializeField] private Vector2 m_Size;
    [Title("")]
    [SerializeField, ReadOnly] private CollectableVisualPart[] m_CollectableVisualParts;

    private CollectableWallet m_Wallet;
    private Coroutine m_Coroutine;
    private Action m_OnEndCallback;
    private int m_Count;

    #region Editor
    [Button]
    private void addVisuals()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        var sprite = transform.parent.FindDeepChild<Image>("Icon").sprite;

        for (int i = 0; i < m_Amount; i++)
        {
            var item = Instantiate(m_CollectableVisualPartPrefab, transform);
            item.GetComponent<RectTransform>().sizeDelta = m_Size;
            item.GetComponent<Image>().sprite = sprite;
            item.name = $"Visual ({i})";
        }

        setRefs();
    }

    [Button]
    private void setRefs()
    {
        m_CollectableVisualParts = GetComponentsInChildren<CollectableVisualPart>(true);
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    private void OnEnable()
    {
        foreach (var visual in m_CollectableVisualParts)
            visual.onEndSending += onEndSending;
    }

    private void OnDisable()
    {
        foreach (var visual in m_CollectableVisualParts)
            visual.onEndSending -= onEndSending;
    }
    #endregion

    #region Callbacks
    private void onEndSending()
    {
        if (m_OnEndCallback == null)
            return;

        m_Count--;
        if (m_Count <= 0)
        {
            m_OnEndCallback?.Invoke();
            m_OnEndCallback = null;
        }
    }
    #endregion

    public void Initialize(CollectableWallet wallet)
    {
        m_Wallet = wallet;
    }

    public void Send(RectTransform start, int amount, RectTransform target, Action onEnd = null)
    {
        if (m_Coroutine != null)
            StopCoroutine(m_Coroutine);

        m_Count = Mathf.Min(amount, m_CollectableVisualParts.Length);
        m_OnEndCallback = onEnd;

        m_Coroutine = StartCoroutine(send(start, target, amount));
    }

    private IEnumerator send(RectTransform start, RectTransform target, int amount)
    {
        int addingValue;
        int lastAddingValue;
        if(amount <= m_CollectableVisualParts.Length)
        {
            addingValue = 1;
            lastAddingValue = addingValue;
        }
        else
        {
            addingValue = amount / m_CollectableVisualParts.Length;
            lastAddingValue = addingValue + amount % m_CollectableVisualParts.Length;
        }
        
        for (int i = 0; i < m_Count; i++)
        {
            var collectable = m_CollectableVisualParts[i];

            collectable.transform.SetParent(target);
            collectable.Initialize(start);
            collectable.MoveTo(target, m_Wallet, (i != m_Count - 1) ? addingValue : lastAddingValue);

            var delay = m_AnimData.DelayBetween * m_AnimData.DelayCurve.Evaluate(((float)i / m_Count));

            if (delay != 0)
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private CollectableAnimationData.SendingData m_AnimData 
        => GameConfig.Instance.CollectableData.CollectableAnimationData.SendingAnimationData;
}
