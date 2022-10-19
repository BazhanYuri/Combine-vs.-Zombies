using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private string _add;
    [SerializeField] private TextUpdatedTrigger _updatedTrigger;
    [SerializeField] private TextMeshProUGUI _text;




    private void OnEnable()
    {
        _updatedTrigger.Updated += UpdateText;
    }
    private void OnDisable()
    {
        _updatedTrigger.Updated -= UpdateText;
    }
    private void UpdateText(int value)
    {
        _text.text = value.ToString() + _add;
    }
}
