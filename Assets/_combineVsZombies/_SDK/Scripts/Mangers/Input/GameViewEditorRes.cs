using UnityEngine;
using System;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using Object = UnityEngine.Object;
#endif

public class GameViewEditorRes
{
#if UNITY_EDITOR
    public static Vector2 GetGameViewScale()
    {
        Type gameViewType = GetGameViewType();
        EditorWindow gameViewWindow = GetGameViewWindow(gameViewType);

        if (gameViewWindow == null)
        {
            Debug.LogError("GameView is null!");
            return Vector2.one;
        }

        var areaField = gameViewType.GetField("m_ZoomArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var areaObj = areaField.GetValue(gameViewWindow);

        var scaleField = areaObj.GetType().GetField("m_Scale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        return (Vector2)scaleField.GetValue(areaObj);
    }

    private static Type GetGameViewType()
    {
        Assembly unityEditorAssembly = typeof(EditorWindow).Assembly;
        Type gameViewType = unityEditorAssembly.GetType("UnityEditor.GameView");
        return gameViewType;
    }

    private static EditorWindow GetGameViewWindow(Type gameViewType)
    {
        Object[] obj = Resources.FindObjectsOfTypeAll(gameViewType);
        if (obj.Length > 0)
        {
            return obj[0] as EditorWindow;
        }
        return null;
    }
#endif
}
