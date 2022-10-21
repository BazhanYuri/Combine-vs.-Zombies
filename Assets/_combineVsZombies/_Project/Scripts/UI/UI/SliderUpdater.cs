using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [SerializeField] private TextUpdatedTrigger _updatedTrigger;
    [SerializeField] private Slider _slider;




    private void OnEnable()
    {
        _updatedTrigger.Updated += UpdateSlider;
    }
    private void OnDisable()
    {
        _updatedTrigger.Updated -= UpdateSlider;
    }
    private void UpdateSlider(int value)
    {
        _slider.value = value / 100f;
    }
}
