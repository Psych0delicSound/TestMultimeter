using UnityEngine;

[CreateAssetMenu(fileName = "MeasurableSO", menuName = "ScriptableObjects/MeasurableSO", order = 1)]
public class MeasurableSO : ScriptableObject
{
    [SerializeField] private float _resistance;
    public float Resistance => _resistance;
    //private float _current;
    //private float _voltage;
    [SerializeField] private float _power;
    public float Power => _power;
    
}