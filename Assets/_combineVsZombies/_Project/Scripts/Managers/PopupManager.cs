using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : PopupManagerBase
{
    #region Init
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.onLevelLoaded += openStartPopup;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.onLevelLoaded -= openStartPopup;
    }
    #endregion

    #region Callbacks
    private void openStartPopup(int _)
    {
        Open(typeof(Popup_LevelStart));
    }
    #endregion
}
