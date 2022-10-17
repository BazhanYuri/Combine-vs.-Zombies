using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Extensions
{
    public static class ButtonExtension
    {
        public static void Set(this Button button, UnityAction action)
        {
            if (button != null && action != null)
            {
                button.onClick.AddListener(action);
            }
        }

        public static void Remove(this Button button, UnityAction action)
        {
            if (button != null && action != null)
            {
                button.onClick.RemoveListener(action);
            }
        }
    }
}


