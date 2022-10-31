using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonImproveContainer : MonoBehaviour
{
    [SerializeField] private Button _improveButton;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private float _improveIndex;

    [SerializeField] private float _cost;
    [SerializeField] private float _costIndex;
    [SerializeField] private int _maxStep;

    [SerializeField] [ReadOnly] private CollectableWallet _collectableWallet;

    private int _indexStep = 0;
    private int _currentCost;

    public float ImproveIndex => _improveIndex;
    public int IndexStep => _indexStep;


    [Button]
    private void SetRefs()
    {
        _collectableWallet = FindObjectOfType<CollectableWallet>();
    }
    private void OnEnable()
    {
        _improveButton.onClick.AddListener(Improve);
        CalculateCurrentCost();
    }
    private void OnDisable()
    {
        _improveButton.onClick.RemoveListener(Improve);
    }
    private void Improve()
    {
        if (_collectableWallet.Amount >= _currentCost)
        {
            _indexStep++;
            _collectableWallet.Add(-_currentCost);
            CalculateCurrentCost();
        }
    }
    private void CalculateCurrentCost()
    {
        if (_indexStep >= _maxStep)
        {
            _costText.text = " ";
            _levelText.text = "Max.";
            _improveButton.GetComponent<Image>().raycastTarget = false;
            _improveButton.interactable = false;
            return;
        }

        _currentCost = (int)(_cost * CalculatingImprovements.CalculateIndex(_costIndex, _indexStep));
        _costText.text = _currentCost.ToString() + " C.";
        _levelText.text = (_indexStep + 1).ToString();
    }
}
