using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableVisualPart : MonoBehaviour
{
    [SerializeField, ReadOnly] private RectTransform m_RectTransform;
    [SerializeField, ReadOnly] private Transform m_Parent;

    private Sequence m_Animation;

    public event Action onEndSending;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Parent = transform.parent;
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    public void Initialize(RectTransform startTransform)
    {
        m_Animation?.Kill();
        m_RectTransform.DOKill();

        transform.position = startTransform.position;

        m_RectTransform.localRotation = Quaternion.identity;
        m_RectTransform.localScale = Vector3.one;
    }

    public void MoveTo(RectTransform target, CollectableWallet wallet, int value)
    {
        gameObject.SetActive(true);
        m_Animation?.Kill();

        m_Animation = DOTween.Sequence();

        m_Animation
            .AppendCallback(() =>
            {
                m_RectTransform.DOScale(Vector2.one * m_AnimData.FirstStage.ScaleFactor, m_AnimData.FirstStage.Time)
                    .SetEase(m_AnimData.FirstStage.ScaleEase);
            })
            .Append(m_RectTransform.DOAnchorPos(getOffset(), m_AnimData.FirstStage.Time)
                .SetEase(m_AnimData.FirstStage.MoveEase)
                .SetRelative(true))
            .Append(m_RectTransform.DOScale(Vector2.one * m_AnimData.SecondStage.ScaleFactor, m_AnimData.SecondStage.Time))
                .SetEase(m_AnimData.SecondStage.ScaleEase)
            .AppendCallback(() =>
            {
                m_RectTransform.DOScale(Vector2.one * m_AnimData.ThirdStage.ScaleFactor, m_AnimData.ThirdStage.Time)
                    .SetEase(m_AnimData.ThirdStage.Ease);
            })
            .AppendInterval(Random.Range(m_AnimData.RandomRange.Min, m_AnimData.RandomRange.Max))
            .Append(m_RectTransform.DOMove(target.position, m_AnimData.ThirdStage.Time)
                .SetEase(m_AnimData.ThirdStage.Ease))
            .AppendCallback(() => wallet.Add(value, false))
            .Append(m_RectTransform.DOPunchScale(Vector3.one * m_AnimData.FinalStage.PunchScaleFactor, m_AnimData.FinalStage.PunchTime))
            .Append(m_RectTransform.DOScale(Vector3.zero, m_AnimData.FinalStage.ScaleTime));

        m_Animation.OnComplete(() =>
        {
            onEndSending?.Invoke();
            transform.SetParent(m_Parent);
            gameObject.SetActive(false);
        });
    }

    private Vector2 getOffset()
    {
        float angle = Random.Range(0, 360f);
        float radius = Random.Range(m_AnimData.Radius * (1f - m_AnimData.RadiusThickness), m_AnimData.Radius);

        return (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right * radius) + m_AnimData.FirstStage.Offset;
    }

    private CollectableAnimationData.SendingData m_AnimData 
        => GameConfig.Instance.CollectableData.CollectableAnimationData.SendingAnimationData;
}
