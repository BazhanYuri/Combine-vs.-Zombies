using UnityEngine;
using UnityEditor;

public class GameConfigEditor : MonoBehaviour
{
    [MenuItem("SDK/Select GameConfig #%t", false, -2)]
    public static void SelectGameConfg()
    {
        Selection.activeObject = GameConfig.Instance;
    }
}