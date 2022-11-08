using UnityEngine;
using DG.Tweening;

public class ImproveVisuals : MonoBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private Transform _visual;
    [SerializeField] private float _scaleIndex;

    [SerializeField] private Vector3 _standartScale = new Vector3(0.1555351f, 0.1555351f, 0.1555351f);

    private void OnEnable()
    {
        _gameStarter.GameStarted += MoveToMachine;
    }
    private void OnDisable()
    {
        _gameStarter.GameStarted -= MoveToMachine;
    }
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
        _visual.DOPunchScale(Vector3.one * (0.15f + (_scaleIndex / 5)), 0.5f, 2).SetEase(Ease.InFlash);
        
        _particleSystem.Play();
    }
   
    private void MoveToMachine()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("StartMove");
        }
    }
}
