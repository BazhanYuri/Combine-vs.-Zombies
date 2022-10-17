using DG.Tweening;
using Extensions;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
    [SerializeField] private Sprite m_BackgroundOffSprite;
    [SerializeField] private Color m_HandleOffColor = new Color();
    [Title("Components")]
    [SerializeField, ReadOnly] private Image m_Handle;
    [SerializeField, ReadOnly] private Image m_Background;
    [SerializeField, ReadOnly] private Button m_Button;

    private float m_ActivePosition;
    private Sprite m_ActiveBackground;
    private Color m_ActiveHandleColor;
    private Tween m_MovingTween;

    private bool m_Value;

    public event Action<bool> onValueChanged;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Background = transform.FindDeepChild<Image>("Switch");
        m_Handle = transform.FindDeepChild<Image>("Handle");
        m_Button = GetComponent<Button>();
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    private void Awake()
    {
        m_Button.Set(onClick);
    }
    #endregion

    #region Callbacks
    private void onClick()
    {
        m_Value = !m_Value;
        playAnimation();
    }

    private void playAnimation()
    {
        m_MovingTween?.Kill();
        m_MovingTween = m_Handle.rectTransform.DOAnchorPosX(m_Value ? m_ActivePosition : -m_ActivePosition, 0.2f)
            .OnComplete(() =>
            {
                updateVisual();
                onValueChanged?.Invoke(m_Value);
            });
    }
    #endregion

    public void SetValue(bool value)
    {
        setActiveVisual();

        m_Value = value;
        updateVisual();
    }

    #region Specific
    private void setActiveVisual()
    {
        if (m_ActiveBackground == null)
        {
            m_ActivePosition = m_Handle.rectTransform.anchoredPosition.x;
            m_ActiveBackground = m_Background.sprite;
            m_ActiveHandleColor = m_Handle.color;
        }
    }

    private void updateVisual()
    {
        if(m_Value)
        {
            m_Background.sprite = m_ActiveBackground;
            m_Handle.color = m_ActiveHandleColor;
            m_Handle.rectTransform.anchoredPosition = new Vector2(m_ActivePosition, m_Handle.rectTransform.anchoredPosition.y);
        }
        else
        {
            m_Background.sprite = m_BackgroundOffSprite;
            m_Handle.color = m_HandleOffColor;
            m_Handle.rectTransform.anchoredPosition = new Vector2(-m_ActivePosition, m_Handle.rectTransform.anchoredPosition.y);
        }
    }
    #endregion
}
