using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class GameConfigBase : SingletonScriptableObject<GameConfig>
{
    [Serializable]
    public abstract class InputVariablesEditorBase
    {
        public Vector2 DragSensitivity = new Vector2(1, 1);

        public bool IsUseJoystick;

        [ShowIf(nameof(IsUseJoystick))] public JoystickData Joystick;

        [Serializable]
        public class JoystickData
        {
            public bool IsShowVisuals = false;

            public bool IsStatic = false;

            public bool IsResetDirection = false;

            public float Radius = 120;
            public float HandleRadiusMultiplier = .25f;
        }

        public bool CalculateMultiTouch = false;
    }

    [Serializable]
    public abstract class AudioVariablesEditorBase
    {
        [Title("Default")]
        public AudioClip UiButtonDefaultSound;
        public AudioClip DefaultSoundtrack;
    }

    [Serializable]
    public abstract class CollectableVariablesEditorBase
    {
        public CollectableAnimationData CollectableAnimationData;
    }

    public abstract class PopupVariablesEditorBase
    {
        public float m_FadeInTime = 0.5f;
        public float m_FadeOutTime = 0.3f;
    }
}


