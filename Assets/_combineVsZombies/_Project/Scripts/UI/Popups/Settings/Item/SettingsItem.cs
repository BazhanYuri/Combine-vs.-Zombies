using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingsItem : MonoBehaviour
{
    [SerializeField, ReadOnly] private Image m_Icon;
    [SerializeField, ReadOnly] private Slider m_Slider;
    [SerializeField, ReadOnly] private Image m_Handle;
    [Title("Visual")]
    [SerializeField] private Sprite m_IconSpriteOff;
    [SerializeField] private Sprite m_HandleSpriteOff;

    private Sprite m_IconSpriteOn, m_HandleSpriteOn;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Icon = transform.FindDeepChild<Image>("Icon");
        m_Slider = GetComponentInChildren<Slider>();
        m_Handle = transform.FindDeepChild<Image>("Handle");
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    protected virtual void Awake()
    {
        m_IconSpriteOn = m_Icon.sprite;

        m_HandleSpriteOn = m_Handle.sprite;
    }

    private void OnEnable()
    {
        setValue(m_AudioData.Value, false);
        m_Slider.onValueChanged.AddListener(setValue);
    }

    private void OnDisable()
    {
        m_Slider.onValueChanged.RemoveListener(setValue);
    }
    #endregion

    #region Callbacks
    private void setValue(float value)
    {
        setValue(value, true);
    }
    #endregion

    protected void setValue(float value, bool isNeedSave)
    {
        m_Slider.value = value;
        
        if(value == 0)
        {
            m_Icon.sprite = m_IconSpriteOff;
            m_Icon.color = m_SettingColor.OffIconColor;

            m_Handle.sprite = m_HandleSpriteOff;
        }
        else
        {
            m_Icon.sprite = m_IconSpriteOn;
            m_Icon.color = m_SettingColor.OnIconColor;

            m_Handle.sprite = m_HandleSpriteOn;
        }

        if (isNeedSave)
            m_AudioData.Value = value;
    }

    protected StorageManagerBase.AudioData m_AudioData;
    private PopupVariablesEditor.SettingColor m_SettingColor => GameConfig.Instance.Popup.SettingIconColor;
}
