using System;
using UnityEngine;

public class TextUpdatedTrigger : MonoBehaviour
{
    public event Action<int> Updated;



    public void InvokeUpdated(int value)
    {
        Updated?.Invoke(value);
    }
}
