using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Extensions;
using System;

public class LevelFailedTimer : MonoBehaviour
{
    [SerializeField] private int m_Seconds = 5;
    [Title("Visual")]
    [SerializeField, ReadOnly] private Image m_FadeBackground;
    [SerializeField, ReadOnly] private TextMeshProUGUI m_Display;

    private Tween m_FadingTween;

    private WaitForSeconds m_Delay = new WaitForSeconds(1f);
    private WaitForSeconds m_SmallDelay = new WaitForSeconds(0.5f);
    private Coroutine m_Coroutine;

    public event Action onTimeRunOut;

    #region Editor
    private void setRefs()
    {
        m_FadeBackground = transform.FindDeepChild<Image>("Fade");
        m_Display = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    #region Init
    private void OnEnable()
    {
        m_Display.text = m_Seconds.ToString();
    }

    private void OnDisable()
    {
        resetTimer();
    }
    #endregion

    public void StartTimer()
    {
        resetTimer();

        m_FadingTween = m_FadeBackground.DOFillAmount(1f, m_Seconds).SetEase(Ease.Linear);
        m_Coroutine = StartCoroutine(startCountdown(m_Seconds));
    }

    public void StopTimer()
    {
        resetTimer();
    }

    #region Specific
    private IEnumerator startCountdown(int seconds)
    {
        m_Display.transform.DOPunchScale(Vector2.one * 0.025f, 0.5f, 2);
        while (seconds > 0)
        {
            yield return m_Delay;
            
            seconds--;
            m_Display.text = seconds.ToString();
            m_Display.transform.DOPunchScale(Vector2.one * 0.025f, 0.5f, 2);
        }

        yield return m_SmallDelay;
        onTimeRunOut?.Invoke();
        m_Coroutine = null;
    }

    private void resetTimer()
    {
        if (m_Coroutine != null)
            StopCoroutine(m_Coroutine);

        m_FadingTween?.Kill();
        m_FadeBackground.fillAmount = 0;
    }
    #endregion
}
