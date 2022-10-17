using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<Vector2> onPointerDown;
    public static event Action<Vector2> onPointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp?.Invoke(eventData.position);
    }
}
