
public class MusicSetting : SettingsItem
{
    protected override void Awake()
    {
        base.Awake();
        m_AudioData = StorageManager.Instance.Music;
    }
}
