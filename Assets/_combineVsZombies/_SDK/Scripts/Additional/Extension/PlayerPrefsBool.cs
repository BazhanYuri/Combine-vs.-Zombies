using UnityEngine;

namespace Extensions
{
    public static class PlayerPrefsBool
    {
        public static bool GetBool(string key, bool Default = false)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) == 1 : Default;
        }

        public static void SetBool(string key, bool state)
        {
            PlayerPrefs.SetInt(key, state ? 1 : 0);
        }
    }
}


