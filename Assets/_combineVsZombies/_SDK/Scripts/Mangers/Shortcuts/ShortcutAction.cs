using System;
using UnityEngine;

public class ShortcutAction
{
    private KeyCode m_KeyCode;
    private Action m_Action;

    public KeyCode KeyCode => m_KeyCode;

    public ShortcutAction(KeyCode keyCode, Action action)
    {
        m_KeyCode = keyCode;
        m_Action = action;
    }

    public void DoAction()
    {
        m_Action?.Invoke();
    }
}