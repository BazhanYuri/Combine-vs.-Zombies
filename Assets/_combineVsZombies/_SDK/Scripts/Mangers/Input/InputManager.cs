using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	[ShowInInspector, ReadOnly] private ScreenData m_ScreenData;
	[SerializeField] private bool LogScreenData = false;

	public ScreenData ScreenData => m_ScreenData;

	#region Simple Input
	private bool m_IsInputDown;
	private Vector3 m_InputDownPosNormalized;
	private Vector3 m_LastInputPosNormalized;
	private Vector2 m_DeltaDrag;
	private Vector2 m_Drag;

	public static Action<Vector2> OnInputDown;
	public static Action<Vector2> OnInputUp;
	public static Action<Vector2> OnDragDelta;
	public static Action<Vector2> OnDrag;

	private Vector3 m_MousePosNormalized => Input.mousePosition / m_ScreenData.FinalDPI;
	public bool IsInputDown => m_IsInputDown;
	public Vector2 DeltaDrag => m_DeltaDrag;
	public Vector2 Drag => m_Drag;
	#endregion

	#region MultiTouchVars
	private List<Touch> m_InitTouchListNormalized = new List<Touch>();
	private List<Touch> m_LastTouchListNormalized = new List<Touch>();
	private List<Touch> m_CurrTouchListNormalized = new List<Touch>();

	private float m_TouchZoom;
	private float m_TouchZoomDelta;

	public static Action<float> OnTouchZoomDelta;
	public static Action<float> OnTouchZoom;

	public float TouchZoomDelta => m_TouchZoomDelta;
	public float TouchZoom => m_TouchZoom;
	#endregion

	#region JoystickVars
	private Vector2 m_JoystickDirection = Vector2.zero;
	private Vector2 m_JoystickPosition = Vector2.zero;
	private Vector2 m_JoystickPositionNormalized => m_JoystickPosition / m_ScreenData.FinalDPI;

	private Vector2 m_JoystickHandleLocalPosition = Vector2.zero;
	//We need to multiply by DPI so we can transform m_JoystickDirection from Physical size to Screen size
	private Vector2 m_JoystickPositionFromMousePosAndDirection => (Vector2)Input.mousePosition - m_JoystickDirection * m_InputVars.Joystick.Radius * m_ScreenData.FinalDPI;

	public Vector2 JoystickDirection => m_JoystickDirection;
	public Vector2 JoystickPosition => m_JoystickPosition;
	public Vector2 JoystickHandleLocalPosition => m_JoystickHandleLocalPosition;
	#endregion

	#region Init
	protected override void OnAwakeEvent()
	{
		base.OnAwakeEvent();

		m_ScreenData = new ScreenData();
		m_ScreenData.CalculateData(LogScreenData);
	
		resetTouchLists();
	}


	private void OnEnable()
	{
        GameManager.onLevelStarted += onLevelStarted;
		InputHandler.onPointerDown += onPointerDown;
		InputHandler.onPointerUp += onPointerUp;
	}

	public override void OnDisable()
	{
		GameManager.onLevelStarted -= onLevelStarted;
		InputHandler.onPointerDown -= onPointerDown;
		InputHandler.onPointerUp -= onPointerUp;
	}
    #endregion

    #region Callbacks
    private void onLevelStarted()
	{
		m_JoystickDirection = Vector2.zero;
		m_JoystickPosition = Vector2.zero;
		m_JoystickHandleLocalPosition = Vector2.zero;

		resetValues();
	}

	private void resetValues()
	{
		m_DeltaDrag = m_Drag = Vector2.zero;
		m_InitTouchListNormalized.Clear();
		m_LastTouchListNormalized.Clear();
	}

	private void onPointerDown(Vector2 position)
	{
		resetValues();
		resetJoystick();

		m_IsInputDown = true;
		m_InputDownPosNormalized = m_LastInputPosNormalized = m_MousePosNormalized;

		OnInputDown?.Invoke(position);
	}

	public void onPointerUp(Vector2 position)
	{
		resetValues();
		resetJoystick();

		m_IsInputDown = false;

		OnInputUp?.Invoke(position);
	}
	#endregion

	#region UnityLoops
	private void Update()
	{
		if (m_InputVars.CalculateMultiTouch)
			calculateMultiTouch();
	}

	private void FixedUpdate()
	{
		if (!m_IsInputDown) return;

		m_Drag = (m_MousePosNormalized - m_InputDownPosNormalized) * m_InputVars.DragSensitivity;
		m_DeltaDrag = (m_MousePosNormalized - m_LastInputPosNormalized) * m_InputVars.DragSensitivity;

		if (m_Drag != Vector2.zero)
			OnDrag?.Invoke(Drag);

		if (m_DeltaDrag != Vector2.zero)
			OnDragDelta?.Invoke(DeltaDrag);

		m_LastInputPosNormalized = m_MousePosNormalized;

		if (m_InputVars.IsUseJoystick) computeJoystick();
	}
	#endregion

	#region MultiTouch
	private void calculateMultiTouch()
	{
		calculateCurrTouchList();

		if (m_CurrTouchListNormalized.Count != m_InitTouchListNormalized.Count)
			resetTouchLists();

		if (m_CurrTouchListNormalized.Count >= 2)
		{
			if ((m_InitTouchListNormalized[0].fingerId != m_CurrTouchListNormalized[0].fingerId) ||
				(m_InitTouchListNormalized[1].fingerId != m_CurrTouchListNormalized[1].fingerId))
			{
				resetTouchLists();
			}
			else
			{
				m_TouchZoom = Vector3.Distance(m_CurrTouchListNormalized[0].position, m_CurrTouchListNormalized[1].position) -
							  Vector3.Distance(m_InitTouchListNormalized[0].position, m_InitTouchListNormalized[1].position);

				m_TouchZoomDelta = Vector3.Distance(m_CurrTouchListNormalized[0].position, m_CurrTouchListNormalized[1].position) -
								   Vector3.Distance(m_LastTouchListNormalized[0].position, m_LastTouchListNormalized[1].position);

				cloneTouchList(ref m_LastTouchListNormalized, m_CurrTouchListNormalized);

				if (m_TouchZoomDelta != 0)
				{
					OnTouchZoom?.Invoke(TouchZoom);
					OnTouchZoomDelta?.Invoke(TouchZoomDelta);
				}
			}
		}
	}

	private void calculateCurrTouchList()
	{
		m_CurrTouchListNormalized.Clear();
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began ||
				touch.phase == TouchPhase.Moved ||
				touch.phase == TouchPhase.Stationary)
			{
				Touch touchNormalized = touch;
				touchNormalized.position /= m_ScreenData.FinalDPI;
				m_CurrTouchListNormalized.Add(touchNormalized);
			}
		}
#if UNITY_EDITOR
		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
		{
			Touch newTouch01 = new Touch { fingerId = 0, position = m_MousePosNormalized };
			m_CurrTouchListNormalized.Add(newTouch01);

			Touch newTouch02 = new Touch { fingerId = 1, position = Vector2.zero };
			m_CurrTouchListNormalized.Add(newTouch02);
		}
#endif
	}

	private void resetTouchLists()
	{
		m_TouchZoom = m_TouchZoomDelta = 0;

		cloneTouchList(ref m_InitTouchListNormalized, m_CurrTouchListNormalized);
		cloneTouchList(ref m_LastTouchListNormalized, m_CurrTouchListNormalized);
	}

	private void cloneTouchList(ref List<Touch> i_TouchesRef, List<Touch> i_ListToCopyFrom)
	{
		i_TouchesRef.Clear();
		i_TouchesRef.AddRange(i_ListToCopyFrom);
	}
	#endregion

	#region Joystick
	private void resetJoystick()
	{
		if (m_InputVars.IsUseJoystick)
		{
			if (m_InputVars.Joystick.IsResetDirection)
			{
				if (m_JoystickDirection != Vector2.zero)
				{
					m_JoystickDirection = m_JoystickDirection.normalized * 0.05f;
					m_JoystickPosition = m_JoystickHandleLocalPosition = Input.mousePosition - (Vector3)m_JoystickDirection;
				}
				else
				{
					m_JoystickDirection = Vector3.zero;
					m_JoystickPosition = m_JoystickHandleLocalPosition = Input.mousePosition;
				}
			}
			else
			{
				m_JoystickPosition = m_JoystickPositionFromMousePosAndDirection;
				m_JoystickHandleLocalPosition = m_JoystickDirection;
			}

			computeJoystick();
		}
	}

	private void computeJoystick()
	{
		m_JoystickDirection = ((Vector2)m_MousePosNormalized - m_JoystickPositionNormalized) / m_JoystickVars.Radius;

		if (m_JoystickVars.IsStatic)
		{
			if (m_JoystickDirection.magnitude > 1)
			{
				m_JoystickDirection.Normalize();
			}
		}
		else
		{
			if (m_JoystickDirection.magnitude > 1)
			{
				m_JoystickDirection.Normalize();
				m_JoystickPosition = m_JoystickPositionFromMousePosAndDirection;
			}
		}

		m_JoystickHandleLocalPosition = m_JoystickDirection;
	}
	#endregion

	private InputVariablesEditor m_InputVars => GameConfig.Instance.Input;
	private InputVariablesEditor.JoystickData m_JoystickVars => GameConfig.Instance.Input.Joystick;
}

