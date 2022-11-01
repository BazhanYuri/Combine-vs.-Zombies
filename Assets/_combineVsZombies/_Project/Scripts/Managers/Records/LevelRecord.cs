using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class LevelRecord : MonoBehaviour
{
    [SerializeField] private Transform _recordLine;
    [SerializeField] private TextMeshProUGUI _textDistance;
    [SerializeField][ReadOnly] private PassedDistanceManager _passedDistanceManager;
    [SerializeField][ReadOnly] private Death _death;


    private int _distanceRecord;

    [Button]
    private void SetRefs()
    {
        _passedDistanceManager = FindObjectOfType<PassedDistanceManager>();
        _death = FindObjectOfType<Death>();
    }


    private void OnEnable()
    {
        _death.onDead += SetNewDistanceRecord;
        _distanceRecord = DataPrefs.GetDistanceRecord();
        _recordLine.position += new Vector3(0, 0, _distanceRecord);
        _textDistance.text = _distanceRecord + " m.";
    }
    private void OnDisable()
    {
        _death.onDead -= SetNewDistanceRecord;
    }



    private void Update()
    {
        
    }
    private void SetNewDistanceRecord()
    {
        int currentPassedDistance = _passedDistanceManager.PassedDistance;

        if (_distanceRecord < currentPassedDistance)
        {
            DataPrefs.SaveDistanceRecord(currentPassedDistance);
        }
    }
}
