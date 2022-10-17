using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class PopupManagerBase : Singleton<PopupManager>
{
    [SerializeField, ReadOnly] private PopupBase[] m_Popups;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Popups = GetComponentsInChildren<PopupBase>(true);
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    protected virtual void OnEnable()
    {
        GameManager.onGameReset += hideAll;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.onGameReset -= hideAll;
    }
    #endregion

    #region Callbacks
    private void hideAll()
    {
        foreach (var popup in m_Popups)
            popup.Close(true);
    }
    #endregion

    #region OpenClose
    public void Open(Type popupType)
    {
        getPopup(popupType)?.Open();
    }

    public void Close(Type popupType)
    {
        getPopup(popupType)?.Close();
    }

    private PopupBase getPopup(Type popupType)
    {
        foreach (var popup in m_Popups)
        {
            if (popup.GetType() == popupType)
            {
                return popup;
            }
        }

        Debug.LogError($"There isn't {popupType}!");
        return null;
    }
    #endregion
}
