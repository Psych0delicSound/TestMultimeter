using System;
using UnityEngine;

public class Device : MonoBehaviour, IMeasurable
{
    [SerializeField] private MeasurableSO data;
    public float GetResistance() => data.Resistance;
    public float GetPower() => data.Power;
}