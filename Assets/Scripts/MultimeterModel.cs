using System;
using UnityEngine;

public enum MultimeterMode
{
    Current,
    Resistance,
    Neutral,
    DirectCurrent,
    AlternatingCurrent
}

public class MultimeterModel : MonoBehaviour
{
    public MultimeterMode ActiveMode { get; private set; }
    public float MeasuredValue { get; private set; }

    public event Action<MultimeterMode> ActiveModeChanged;
    public event Action<float> MeasuredValueChanged;


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

    public void SetMeasuredValue()
    {

    }

}
