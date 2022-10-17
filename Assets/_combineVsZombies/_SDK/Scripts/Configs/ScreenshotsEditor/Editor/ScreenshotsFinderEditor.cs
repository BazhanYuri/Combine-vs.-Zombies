using UnityEditor;
using UnityEngine;

public class ScreenshotsFinderEditor : MonoBehaviour
{
    [MenuItem("SDK/Select ScreenshotsMaker #%w", false, -2)]
    public static void SelectGameConfg()
    {
        var screnshots = Resources.FindObjectsOfTypeAll<ScreenshotsEditor>()[0];
        if(screnshots == null) 
            Selection.activeObject = screnshots;
    }
}
