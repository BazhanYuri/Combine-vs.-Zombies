using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SettingVibration : MonoBehaviour
{
    [SerializeField, ReadOnly] private Image m_Icon;
    [SerializeField, ReadOnly] private SettingToggle m_Toggle;
    [Title("Visua")]
    [SerializeField] private Sprite m_IconOff;

    private Sprite m_ActiveIcon;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Icon = transform.FindDeepChild<Image>("Icon");
        m_Toggle = GetComponentInChildren<SettingToggle>(true);
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    private void Awake()
    {
        m_ActiveIcon = m_Icon.sprite;
    }

    private void OnEnable()
    {
        m_Toggle.onValueChanged += onValueChanged;

        bool currentState = m_StorageManager.IsVibrationOn;
        m_Toggle.SetValue(currentState);
        updateVisual(currentState);
    }

    private void OnDisable()
    {
        m_Toggle.onValueChanged -= onValueChanged;
    }
    #endregion

    #region Callbacks
    private void onValueChanged(bool value)
    {
        m_StorageManager.IsVibrationOn = value;

        updateVisual(value);
    }

    private void updateVisual(bool value)
    {
        m_Icon.sprite = value ? m_ActiveIcon : m_IconOff;
        m_Icon.color = value ? m_SettingColor.OnIconColor : m_SettingColor.OffIconColor;
    }
    #endregion

    private StorageManager m_StorageManager => StorageManager.Instance;
    private PopupVariablesEditor.SettingColor m_SettingColor => GameConfig.Instance.Popup.SettingIconColor;
}
