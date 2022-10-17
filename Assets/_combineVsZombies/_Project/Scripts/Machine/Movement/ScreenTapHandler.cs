using System;
using UnityEngine;


public enum SideTouched
{
    Left,
    Right,
    None
}
public class ScreenTapHandler : MonoBehaviour
{
    public event Action<SideTouched> ScreenTouchedCallback;




    
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        if (Input.touchCount <= 0)
        {
            ScreenTouchedCallback?.Invoke(SideTouched.None);
            return;
        }
        Vector2 touchPos = Input.GetTouch(0).position;
        float screenWidth = Screen.width;
        float onePercent = screenWidth / 100f;
        float currentPercentage = touchPos.x / onePercent;

        if (currentPercentage < 50)
        {
            ScreenTouchedCallback?.Invoke(SideTouched.Left);
            print("left");
        }
        else
        {
            ScreenTouchedCallback?.Invoke(SideTouched.Right);
            print("right");
        }
    }
}
