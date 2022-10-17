using UnityEditor;
using UnityEngine;

public class StorageManagerEditor : MonoBehaviour
{
    [MenuItem("SDK/Select StorageManager #%h", false, -2)]
    public static void SelectStorageManager()
    {
        Selection.activeObject = StorageManager.Instance;
    }
}
