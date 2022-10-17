using System;
using UnityEngine;

public class GameConfig : GameConfigBase
{
    public InputVariablesEditor Input = new InputVariablesEditor();
    public AudioVariablesEditor Audio = new AudioVariablesEditor();
    public CollectableVariablesEditor CollectableData = new CollectableVariablesEditor();
    public PopupVariablesEditor Popup = new PopupVariablesEditor();
}

[Serializable]
public class InputVariablesEditor : GameConfigBase.InputVariablesEditorBase
{
    //
}

[Serializable]
public class AudioVariablesEditor : GameConfigBase.AudioVariablesEditorBase
{
    //
}

[Serializable]
public class CollectableVariablesEditor : GameConfigBase.CollectableVariablesEditorBase
{
    //
}

[Serializable]
public class PopupVariablesEditor: GameConfigBase.PopupVariablesEditorBase
{
    public SettingColor SettingIconColor;
    public WinPopupData WinPopup;

    [Serializable]
    public class SettingColor
    {
        public Color OnIconColor = new Color();
        public Color OffIconColor = new Color();
    }

    [Serializable]
    public class WinPopupData
    {
        public Sprite FullStar;
        public Sprite EmptySprite;
    }
}