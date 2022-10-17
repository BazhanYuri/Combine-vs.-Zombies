using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour
{
    [SerializeField, ReadOnly] private Image m_JoystickOutline;
    [SerializeField, ReadOnly] private Image m_JoystickHandle;

    [SerializeField, ReadOnly] private Canvas m_Canvas;
    [SerializeField, ReadOnly] private CanvasScaler m_CanvasScaler;

    private float m_CurrentMatchWidthOrHeight;

    private InputVariablesEditor m_InputVars => GameConfig.Instance.Input;

    #region Editor
    [Button]
    public void SetRefs()
    {
        m_JoystickOutline = transform.FindDeepChild<Image>("Outline");
        m_JoystickHandle = transform.FindDeepChild<Image>("Handle");
        m_Canvas = transform.GetComponentInParent<Canvas>();
        m_CanvasScaler = transform.GetComponentInParent<CanvasScaler>();
    }
    #endregion

    #region Init
    private void OnEnable()
    {
        InputManager.OnInputDown += onInputDown;
        InputManager.OnInputUp += onInputUp;

        if (!m_InputVars.IsUseJoystick || !m_InputVars.Joystick.IsShowVisuals)
        {
            gameObject.SetActive(false);
        }

        m_CurrentMatchWidthOrHeight = m_CanvasScaler.matchWidthOrHeight;
        setSizes();
    }

    private void OnDisable()
    {
        InputManager.OnInputDown -= onInputDown;
        InputManager.OnInputUp -= onInputUp;
    }
    #endregion

    #region Callbacks
    private void onInputDown(Vector2 i_Pos)
    {
        if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
        {
            m_JoystickOutline.gameObject.SetActive(true);
        }
    }

    private void onInputUp(Vector2 _)
    {
        if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
        {
            m_JoystickOutline.gameObject.SetActive(false);
        }
    }
    #endregion

    private void Update()
    {
        if (m_CurrentMatchWidthOrHeight != m_CanvasScaler.matchWidthOrHeight)
        {
            m_CurrentMatchWidthOrHeight = m_CanvasScaler.matchWidthOrHeight;
            setSizes();
        }

        if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
        {
            var screenSize = Mathf.Lerp(InputManager.Instance.ScreenData.ScreenSizeInch.x, InputManager.Instance.ScreenData.ScreenSizeInch.y, m_CanvasScaler.matchWidthOrHeight);
            m_JoystickOutline.rectTransform.localScale = Vector3.one / screenSize * m_InputVars.Joystick.Radius * 2;
            m_JoystickOutline.rectTransform.anchoredPosition = InputManager.Instance.JoystickPosition / m_Canvas.scaleFactor;
            m_JoystickHandle.rectTransform.anchoredPosition = InputManager.Instance.JoystickHandleLocalPosition * m_JoystickOutline.rectTransform.sizeDelta.y * .5f;
        }
    }

    private void setSizes()
    {
        m_JoystickOutline.rectTransform.sizeDelta = Vector2.one * Mathf.Lerp(m_CanvasScaler.referenceResolution.x, m_CanvasScaler.referenceResolution.y, m_CanvasScaler.matchWidthOrHeight);
        m_JoystickHandle.rectTransform.sizeDelta = m_JoystickOutline.rectTransform.sizeDelta * m_InputVars.Joystick.HandleRadiusMultiplier;
    }
}
