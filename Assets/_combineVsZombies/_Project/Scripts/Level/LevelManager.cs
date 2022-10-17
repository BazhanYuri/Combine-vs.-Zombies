using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField, ReadOnly] private List<Level> m_Levels;

    private Level m_CurrentLevel;

    public static event Action onLevelAdded;

    #region Editor
    [Button]
    private void setRefs()
    {
        m_Levels = GetComponentsInChildren<Level>(true).ToList();
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
        foreach (var level in m_Levels)
            level.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.onLevelLoaded += onLevelLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.onLevelLoaded -= onLevelLoaded;
    }
    #endregion

    #region Callbacks
    private void onLevelLoaded(int level)
    {
        if (m_CurrentLevel != null)
            m_CurrentLevel.gameObject.SetActive(false);

        m_CurrentLevel = m_Levels[(level - 1) % m_Levels.Count];
        m_CurrentLevel.gameObject.SetActive(true);

        onLevelAdded?.Invoke();
    }
    #endregion
}
