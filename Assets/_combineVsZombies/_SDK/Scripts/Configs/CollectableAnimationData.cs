using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class CollectableAnimationData : ScriptableObject
{
    [Title("Collectable")]
    [SerializeField] private SendingData m_SendingAnimationData;
    [SerializeField] private WalletData m_WalletAnimationData;

    public SendingData SendingAnimationData => m_SendingAnimationData;
    public WalletData WalletAnimationData => m_WalletAnimationData;


    [System.Serializable]
    public class SendingData
    {
        [field: SerializeField] public float DelayBetween { get; private set; }
        [field: SerializeField] public AnimationCurve DelayCurve { get; private set; }
        [Title("Radius")]
        [SerializeField] private float m_Radius; public float Radius => m_Radius;
        [field: SerializeField] public float RadiusThickness { get; private set; }

        [Title("FirstStage")]
        [SerializeField] private FirstStageData m_FirstStage; public FirstStageData FirstStage => m_FirstStage;
        [SerializeField] private SecondStageData m_SecondStage; public SecondStageData SecondStage => m_SecondStage;
        [SerializeField] private RandomRangeDelay m_RandomRangeDalay; public RandomRangeDelay RandomRange => m_RandomRangeDalay;
        [SerializeField] private ThirdStageData m_ThirdStage; public ThirdStageData ThirdStage => m_ThirdStage;
        [SerializeField] private FinalStageData m_FinalStage; public FinalStageData FinalStage => m_FinalStage;

        [System.Serializable]
        public class FirstStageData
        {
            [field: SerializeField] public float Time { get; private set; } = 0.15f;
            [field: SerializeField] public float ScaleFactor { get; private set; } = 0.8f;
            [field: SerializeField] public Vector2 Offset { get; private set; } = new Vector2(0, 100);
            [field: SerializeField] public Ease ScaleEase { get; private set; } = Ease.OutBack;
            [field: SerializeField] public Ease MoveEase { get; private set; } = Ease.OutQuad;
        }

        [System.Serializable]
        public class SecondStageData
        {
            [field: SerializeField] public float Time { get; private set; } = 0.2f;
            [field: SerializeField] public float ScaleFactor { get; private set; } = 1f;
            [field: SerializeField] public Ease ScaleEase { get; private set; } = Ease.InSine;
        }

        [System.Serializable]
        public class RandomRangeDelay
        {
            [field: SerializeField] public float Min { get; private set; } = 0f;
            [field: SerializeField] public float Max { get; private set; } = 0.5f;
        }

        [System.Serializable]
        public class ThirdStageData
        {
            [field: SerializeField] public float Time { get; private set; } = 0.5f;
            [field: SerializeField] public float ScaleFactor { get; private set; } = 0.6f;
            [field: SerializeField] public Ease Ease { get; private set; } = Ease.InSine;
        }

        [System.Serializable]
        public class FinalStageData
        {
            [field: SerializeField] public float PunchTime { get; private set; } = 0.5f;
            [field: SerializeField] public float PunchScaleFactor { get; private set; } = 0.25f;
            [field: SerializeField] public float ScaleTime { get; private set; } = 0.25f;
        }
    }

    [System.Serializable]
    public class WalletData
    {
        [field: SerializeField] public AnimationData AmountAnimation { get; private set; }
        [field: SerializeField] public AnimationData IconAnimation { get; private set; }

        [System.Serializable]
        public class AnimationData
        {
            [field: SerializeField] public Vector3 Scale { get; private set; }
            [field: SerializeField] public float Time { get; private set; }
            [field: SerializeField] public int Vibration { get; private set; }
        }
    }
}
