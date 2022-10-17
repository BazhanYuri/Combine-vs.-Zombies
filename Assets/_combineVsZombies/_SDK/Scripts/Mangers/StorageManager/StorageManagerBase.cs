using Extensions;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class StorageManagerBase : Singleton<StorageManager>
{
    #region Settings
    [Title("Settings")]
    [ShowInInspector, PropertyOrder(1)] public bool IsVibrationOn 
    { 
        get { return PlayerPrefsBool.GetBool(nameof(IsVibrationOn), true); } 
        set { PlayerPrefsBool.SetBool(nameof(IsVibrationOn), value); } 
    }

    #region SoundMusic
    [SerializeField, PropertyOrder(2)] private AudioData m_Sound = new AudioData("Sound");
    [SerializeField, PropertyOrder(2)] private AudioData m_Music = new AudioData("Music");

    public AudioData Sound => m_Sound;
    public AudioData Music => m_Music;
    #endregion

    #endregion

    #region Collectables
    public virtual int GetCollectable(eCollectable eCollectable)
    {
        return PlayerPrefs.GetInt(eCollectable.ToString(), 0);
    }

    public virtual void SetCollectable(eCollectable eCollectable, int amount)
    {
        PlayerPrefs.SetInt(eCollectable.ToString(), amount);
    }
    #endregion

    #region Data
    [Serializable]
    public class AudioData
    {
        [SerializeField, ReadOnly] private string m_SavingName;
        
        public Action<float> onValueChanged;

        public AudioData(string savingName)
        {
            m_SavingName = savingName;
        }

        [ShowInInspector]
        public float Value
        {
            get 
            { 
                return PlayerPrefs.GetFloat(m_SavingName, 0.5f); 
            }

            set 
            {
                onValueChanged?.Invoke(value);
                PlayerPrefs.SetFloat(m_SavingName, value); 
            }
        }

        public bool IsOn => Value > 0f;
    }
    #endregion
}


