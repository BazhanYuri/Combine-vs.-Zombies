using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticManager : Singleton<HapticManager>
{
    public void Haptic(HapticTypes i_Haptic, bool defaultToRegularVibrate = false, bool allowVibrationOnLegacyDevices = true)
    {
        if (Managers.Instance != null && StorageManager.Instance.IsVibrationOn)
        {
            MMVibrationManager.Haptic(i_Haptic, defaultToRegularVibrate, allowVibrationOnLegacyDevices);
        }
    }


    // Dummy function so Unity will add to AndroidManifest.xml
    // <uses-permission android:name="android.permission.VIBRATE" />
    public void DummyFunction()
    {
        Handheld.Vibrate();
    }
}
