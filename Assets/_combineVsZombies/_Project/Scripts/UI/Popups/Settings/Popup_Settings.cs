using Extensions;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Settings : PopupBase
{
    [SerializeField, ReadOnly] private Button m_CloseButton;

    public static event Action onClose;

    #region Editor
    protected override void setRefs()
    {
        base.setRefs();
        m_CloseButton = transform.FindDeepChild<Button>("Button_Close");
    }
    #endregion

    #region Init
    private void Awake()
    {
        m_CloseButton.Set(close);
    }
    #endregion

    #region Callbacks
    private void close()
    {
        m_CloseButton.interactable = false;
        Close();
    }
    #endregion

    protected override void onEndOpeningAnimation()
    {
        base.onEndOpeningAnimation();
        m_CloseButton.interactable = true;
    }

    protected override void onEndClosingAnimation()
    {
        base.onEndClosingAnimation();
        onClose?.Invoke();
    }
}
