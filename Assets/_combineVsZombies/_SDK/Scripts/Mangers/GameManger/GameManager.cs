using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<int> onLevelLoaded;
    public static event Action onLevelStarted;
    public static event Action onLevelCompleted;
    public static event Action onLevelFailed;
    public static event Action onLevelContinued;
    public static event Action onGameReset;

    private bool m_CanContinue;
    private LevelSaver m_LevelSaver;

    public int CurrentLevel
    {
        get
        {
            if (m_LevelSaver == null)
            {
                m_LevelSaver = new LevelSaver();
                return m_LevelSaver.LoadLevel();
            }

            return m_LevelSaver.CurrentLevel;
        }
    }

    #region Editor
    [Button]
    private void WinLevel() => LevelCompleted();
    [Button]
    private void LoseLevel() => LevelFailed();

    public void NextLevel()
    {
        m_LevelSaver.AddLevelAndSave();
        LoadLevel();
    }

    public void PreviousLevel()
    {
        m_LevelSaver.RemoveLevelAndSave();
        LoadLevel();
    }
    #endregion

    #region Init
    protected override void OnAwakeEvent()
    {
        base.OnAwakeEvent();

        if (m_LevelSaver == null)
        {
            m_LevelSaver = new LevelSaver();
        }
    }

    public override void Start()
    {
        base.Start();
        resetGame();
        StartCoroutine(waitAndLoadStartLevel());
    }

    private IEnumerator waitAndLoadStartLevel()
    {
        yield return null;

        onLevelLoaded?.Invoke(m_LevelSaver.LoadLevel());
        m_GameTransition.DoStartTransition();
    }
    #endregion

    #region LoadLevel
    public void LoadLevel()
    {
        m_GameTransition.DoTransition(() => StartCoroutine(waitAndLoadLevel()));
    }

    private IEnumerator waitAndLoadLevel()
    {
        resetGame();
        yield return null;
        onLevelLoaded?.Invoke(m_LevelSaver.LoadLevel());
    }

    private void resetGame()
    {
        m_CanContinue = true;
        onGameReset?.Invoke();
    }
    #endregion

    public void StartLevel()
    {
        onLevelStarted?.Invoke();
    }

    public void LevelCompleted()
    {
        onLevelCompleted?.Invoke();

        PopupManager.Instance.Open(typeof(Popup_LevelWin));
        m_LevelSaver.AddLevelAndSave();
    }

    public void LevelFailed(bool withContinue = true)
    {
        if (withContinue == true && m_CanContinue == true)
        {
            m_CanContinue = false;
            PopupManager.Instance.Open(typeof(Popup_LevelFailedWithContinue));
        }
        else
        {
            PopupManager.Instance.Close(typeof(Popup_LevelFailedWithContinue));
            PopupManager.Instance.Open(typeof(Popup_LevelFailed));
        }

        onLevelFailed?.Invoke();
    }

    public virtual void LevelContinued()
    {
        m_CanContinue = false;
        onLevelContinued?.Invoke();
    }

    private GameTransition m_GameTransition => GameTransition.Instance;
}

public class LevelSaver
{
    private readonly string m_LevelSavingPath = "LevelSaving";

    private int m_CurrentLevel;

    public int CurrentLevel => m_CurrentLevel;

    public int LoadLevel()
    {
        m_CurrentLevel = PlayerPrefs.GetInt(m_LevelSavingPath, 1);
        return m_CurrentLevel;
    }

    public void AddLevelAndSave()
    {
        m_CurrentLevel++;
        PlayerPrefs.SetInt(m_LevelSavingPath, m_CurrentLevel);
    }

    public void RemoveLevelAndSave()
    {
        m_CurrentLevel--;
        PlayerPrefs.SetInt(m_LevelSavingPath, Mathf.Max(m_CurrentLevel, 0));
    }
}