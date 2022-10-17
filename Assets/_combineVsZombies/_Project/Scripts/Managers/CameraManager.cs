using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private Camera m_MainCamera;

    public Camera MainCamera => m_MainCamera;

    [Button]
    private void setRef()
    {
        m_MainCamera = transform.FindDeepChild<Camera>("Main Camera");
    }
}
