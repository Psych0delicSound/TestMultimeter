using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private MultimeterModel model;
    [SerializeField] private MultimeterView view;
    [SerializeField] private Regulator regulator;


    private void Start()
    {
        if (regulator == null || model == null || regulator == null)
        {
            Debug.LogError("Some parameters wasn't set");
            return;
        }

        regulator.Rotated += model.SetActiveMode;
            
        model.ActiveModeChanged += view.UpdateHUDActiveMode;
        model.ActiveModeChanged += OnActiveModeChanged; // Fixed subscription
        model.MeasuredValueChanged += view.UpdateDisplayActiveValue;
        model.MeasuredValueChanged += view.UpdateHUDActiveValue;

        model.SetActiveMode(0);
    }

    private void OnDestroy()
    {
        regulator.Rotated -= model.SetActiveMode;

        model.ActiveModeChanged -= view.UpdateHUDActiveMode;
        model.ActiveModeChanged -= OnActiveModeChanged;
        model.MeasuredValueChanged -= view.UpdateDisplayActiveValue;
        model.MeasuredValueChanged -= view.UpdateHUDActiveValue;
    }

    private void OnActiveModeChanged(MultimeterMode mode)
    {
        float calculatedValue = GetRequiredCalculation();
        model.SetMeasuredValue(calculatedValue);
    }

    public float GetRequiredCalculation()
    {
        switch (model.ActiveMode)
        {
            case MultimeterMode.Neutral:
                return CalculatedNeutral();

            case MultimeterMode.Resistance:
                return CalculatedResistance();

            case MultimeterMode.Current:
                return CalculatedCurrent();

            case MultimeterMode.DirectCurrentVoltage:
                return CalculatedDirectCurrentVoltage();

            case MultimeterMode.AlternatingCurrentVoltage:
                return CalculatedAlternatingCurrentVoltage();
        }

        Debug.LogError($"There no {model.ActiveMode} mode");
        return 0;
    }

    private float CalculatedNeutral()
    {
        return 0;
    }

    private float CalculatedResistance()
    {
        float resistance = model.MeasurableOnProbe1.GetResistance();

        return resistance;
    }

    private float CalculatedCurrent()
    {
        float power =  model.MeasurableOnProbe1.GetPower();
        float resistance =  model.MeasurableOnProbe1.GetResistance();

        return Mathf.Sqrt(power / resistance);
    }

    private float CalculatedDirectCurrentVoltage()
    {
        float power =  model.MeasurableOnProbe1.GetPower();
        float resistance =  model.MeasurableOnProbe1.GetResistance();

        return Mathf.Sqrt(power * resistance);
    }

    private float CalculatedAlternatingCurrentVoltage()
    {
        // Static value as written in specs because there not enough variables
        return 0.01f;
    }

}
