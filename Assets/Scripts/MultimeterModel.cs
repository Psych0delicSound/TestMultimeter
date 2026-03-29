using System;
using UnityEngine;

public enum MultimeterMode
{
    Current,
    Resistance,
    Neutral,
    DirectCurrentVoltage,
    AlternatingCurrentVoltage
}

public class MultimeterModel : MonoBehaviour
{
    public MultimeterMode ActiveMode { get; private set; }
    public float MeasuredValue { get; private set; }

    public event Action<MultimeterMode> ActiveModeChanged;
    public event Action<float> MeasuredValueChanged;
    

    [SerializeField] private Device _measurableOnProbe1;/*,
                                    measurableOnProbe2;*/
/*  I found multimeter should output differnce from two probes,
    but i am not sure about device implementation, so now it just taking one value */
    public Device MeasurableOnProbe1 => _measurableOnProbe1;


    [Serializable]
    private class DivisionAngleWithMode
    {
        public float angleMax;
        public MultimeterMode multimeterMode;
    }

    [SerializeField] private DivisionAngleWithMode[] divisions;


    public void SetActiveMode(float regulatorAngle)
    {
        foreach (DivisionAngleWithMode division in divisions)
        {           
            if (regulatorAngle > division.angleMax)
                continue;

            if (ActiveMode == division.multimeterMode)
                break;

            ActiveMode = division.multimeterMode;
            ActiveModeChanged?.Invoke(ActiveMode);
            Debug.Log($"ActiveMode == {ActiveMode}");

            break;
        }
    }

    public void SetMeasuredValue(float newValue)
    {
        MeasuredValue = newValue;
        MeasuredValueChanged?.Invoke(MeasuredValue);
    }

}
