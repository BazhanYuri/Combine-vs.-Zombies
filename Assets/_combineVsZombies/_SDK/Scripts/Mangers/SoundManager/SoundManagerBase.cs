using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SoundManagerBase : Singleton<SoundManager>
{
	[Title("Audio Sources")]
	[SerializeField, ReadOnly] protected AudioSource m_MusicSource;
	[SerializeField, ReadOnly] protected AudioSource m_SoundSource;
	[SerializeField, ReadOnly] protected AudioSource m_UiSoundSource;

    #region States
    [ShowInInspector, ReadOnly, BoxGroup("States")]
	public bool IsMusicMuted
	{
		get
		{
			return !m_StorageManager.Music.IsOn;
		}
	}

	[ShowInInspector, ReadOnly, BoxGroup("States")]
	public bool IsSFXMuted
	{
		get
		{
			return !m_StorageManager.Sound.IsOn;
		}
	}
    #endregion

    #region Editor
	protected virtual void setRefs()
    {
		m_MusicSource = transform.FindDeepChild<AudioSource>("Music Source");
		m_SoundSource = transform.FindDeepChild<AudioSource>("Sound Source");
		m_UiSoundSource = transform.FindDeepChild<AudioSource>("UI Sound Source");
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
		updateAudioSourcesMuteState();
	}

	protected virtual void OnEnable()
	{
		m_StorageManager.Music.onValueChanged += onMusicValueChanged;
		m_StorageManager.Sound.onValueChanged += onSoundValueChanged;
	}

	public override void OnDisable()
	{
		base.OnDisable();

		m_StorageManager.Music.onValueChanged -= onMusicValueChanged;
		m_StorageManager.Sound.onValueChanged -= onSoundValueChanged;
	}
	#endregion

	#region Callbacks
	private void updateAudioSourcesMuteState()
	{
		onMusicValueChanged(m_StorageManager.Music.Value);
		onSoundValueChanged(m_StorageManager.Sound.Value);
	}

	private void onMusicValueChanged(float value)
    {
		m_MusicSource.volume = value;
		m_MusicSource.mute = IsMusicMuted;
	}

	private void onSoundValueChanged(float value)
    {
		m_SoundSource.volume = value;
		m_UiSoundSource.volume = value;

		m_UiSoundSource.mute = m_SoundSource.mute = IsSFXMuted;
	}
	#endregion

	#region Sounds
	public virtual void PlayUIClickSound(AudioClip clickSound = null)
	{
		if (IsSFXMuted)
			return;

		if (clickSound != null)
		{
			m_UiSoundSource.PlayOneShot(clickSound);
		}
		else
        {
			if(m_UiSoundSource.clip != null)
            {
				m_UiSoundSource.PlayOneShot(m_Audio.UiButtonDefaultSound);
			}
			else
            {
				Debug.LogError("Button click sound wasn't set!");
            }
		}
	}

	public virtual void PlaySFX(AudioClip audioClip)
	{
		if(IsSFXMuted)
			return;

		m_SoundSource.PlayOneShot(audioClip, m_StorageManager.Sound.Value);
	}
	#endregion

	#region Music
	public void PlayMusic(AudioClip soundtrack = null)
    {
		if (IsMusicMuted)
			return;

        if (soundtrack != null)
        {
            m_MusicSource.clip = soundtrack;
        }
        else
        {
            m_MusicSource.clip = m_Audio.DefaultSoundtrack;
        }

        playMusic();
    }

    private void playMusic()
    {
        if (m_MusicSource.isPlaying == false)
        {
            if (m_MusicSource.clip != null)
            {
                m_MusicSource.Play();
            }
            else
            {
                Debug.LogError("Music audio clip wasn't set!");
            }
        }
    }
    #endregion

    protected StorageManager m_StorageManager => StorageManager.Instance;
	protected AudioVariablesEditor m_Audio => GameConfig.Instance.Audio;
}
