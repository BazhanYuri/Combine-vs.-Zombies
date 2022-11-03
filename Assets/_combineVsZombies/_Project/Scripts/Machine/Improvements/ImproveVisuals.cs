using UnityEngine;
using DG.Tweening;

public class ImproveVisuals : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _visual;
    [SerializeField] private float _scaleIndex;


    private Vector3 _standartScale;

    public void SetSizeOfVisual(int improveIndex)
    {
        if (improveIndex > 0)
        {
            Vector3 newScale = _standartScale * (1 + (_scaleIndex * improveIndex));
            _visual.localScale = newScale;
        }
    }
    public void ImproveEffect(int improveIndex)
    {
        SetSizeOfVisual(improveIndex);
        _visual.DOPunchScale(Vector3.one * 0.1f, 0.2f * improveIndex, 3);
    }


    private void OnEnable()
    {
        _standartScale = _visual.localScale;
    }
}
