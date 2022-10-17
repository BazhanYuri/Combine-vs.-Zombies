using Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : Singleton<HudManager>
{
    [SerializeField, ReadOnly] private TextMeshProUGUI m_LevelDisplay;
    [SerializeField, ReadOnly] private Button m_ButtonSettings;

    private const string k_LevelStringFormat = "Level {0}";

    #region Editor
    [Button]
    private void setRefs()
    {
        m_LevelDisplay = transform.FindDeepChild<TextMeshProUGUI>("LevelDisplay");
        m_ButtonSettings = transform.FindDeepChild<Button>("Button_Settings");
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    protected override void OnAwakeEvent()
    {
        base.OnAwakeEvent();
        m_ButtonSettings.Set(onSettingTap);
    }

    private void OnEnable()
    {
        updateLevel(m_GameManager.CurrentLevel);
        GameManager.onGameReset += onGameReset;
        GameManager.onLevelLoaded += updateLevel;
        GameManager.onLevelStarted += hideSettingButton;
        GameManager.onLevelCompleted += hideLevelTitle;
        GameManager.onLevelFailed += hideLevelTitle;
        GameManager.onLevelContinued += showLevelTitle;

        Popup_Settings.onClose += showSettingButton;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.onGameReset -= onGameReset;
        GameManager.onLevelLoaded -= updateLevel;
        GameManager.onLevelStarted -= hideSettingButton;
        GameManager.onLevelCompleted -= hideLevelTitle;
        GameManager.onLevelFailed -= hideLevelTitle;
        GameManager.onLevelContinued -= showLevelTitle;

        Popup_Settings.onClose -= showSettingButton;
    }
    #endregion

    #region Callbacks
    private void updateLevel(int level)
    {
        m_LevelDisplay.text = k_LevelStringFormat.Formatted(level);
    }

    private void onGameReset()
    {
        showSettingButton();
        showLevelTitle();
    }

    private void onSettingTap()
    {
        m_ButtonSettings.gameObject.SetActive(false);
        PopupManager.Instance.Open(typeof(Popup_Settings));
    }

    private void showSettingButton()
    {
        m_ButtonSettings.gameObject.SetActive(true);
    }

    private void hideSettingButton()
    {
        m_ButtonSettings.gameObject.SetActive(false);
    }

    private void showLevelTitle()
    {
        m_LevelDisplay.gameObject.SetActive(true);
    }

    private void hideLevelTitle()
    {
        m_LevelDisplay.gameObject.SetActive(false);
    }
    #endregion

    private GameManager m_GameManager => GameManager.Instance;
}
