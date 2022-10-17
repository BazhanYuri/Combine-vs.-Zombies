using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    [SerializeField, ReadOnly] private ScriptableObject[] DummyReferences;

    private void OnValidate()
    {
        DummyReferences = Resources.FindObjectsOfTypeAll<ScriptableObject>()
                   .Where(x => /*x.name.Contains("GameSettings") ||
                               x.name.Contains("Plugins") ||*/
                               x.name.Contains("GameConfig")).ToArray(); //TODO
    }
}
