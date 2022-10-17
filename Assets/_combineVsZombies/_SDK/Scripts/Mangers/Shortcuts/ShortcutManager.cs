using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
#if UNITY_EDITOR
    private List<ShortcutAction> m_Shortcuts;

    private void Awake()
    {
        m_Shortcuts = new List<ShortcutAction>()
        {
            new ShortcutAction(KeyCode.W, GameManager.Instance.LevelCompleted),
            new ShortcutAction(KeyCode.L, () => GameManager.Instance.LevelFailed()),
            new ShortcutAction(KeyCode.E, GameManager.Instance.NextLevel),
            new ShortcutAction(KeyCode.Q, GameManager.Instance.PreviousLevel),
            new ShortcutAction(KeyCode.R, GameManager.Instance.LoadLevel)
        };
    }

    private void Update()
    {
        foreach (var shortcut in m_Shortcuts)
        {
            if (Input.GetKeyDown(shortcut.KeyCode))
                shortcut.DoAction();
        }
    }
#endif
}
