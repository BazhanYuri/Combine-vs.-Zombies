
public class SoundSetting : SettingsItem
{
    protected override void Awake()
    {
        base.Awake();
        m_AudioData = StorageManager.Instance.Sound;
    }
}
