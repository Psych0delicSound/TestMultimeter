using System;
using TMPro;
using UnityEngine;

public class MultimeterView : MonoBehaviour
{
    [SerializeField] private TextMeshPro textDisplayActive;
    
    [Serializable]
    private class HUDForCertainMode
    {
        [NonSerialized] public bool isActive;
        public char symbol;
        public TextMeshProUGUI textUGUI;
        public MultimeterMode requiredMode;
    }
    
    [SerializeField] private HUDForCertainMode[] ArrayHUD;

    public void UpdateDisplayActiveValue(float measuredValue)
    {
        if (textDisplayActive != null)
            textDisplayActive.text = $"{measuredValue:F2}";
    }

    public void UpdateHUDActiveValue(float measuredValue)
    {
        if (ArrayHUD == null) return;

        foreach (HUDForCertainMode HUD in ArrayHUD)
        {
            if (HUD.textUGUI == null) continue;

            float newValue = HUD.isActive ? measuredValue : 0;
            HUD.textUGUI.text = $"{HUD.symbol}: {newValue:F2}";
        }
    }

    public void UpdateHUDActiveMode(MultimeterMode activeMode)
    {
        if (ArrayHUD == null) return;

        foreach (HUDForCertainMode HUD in ArrayHUD)
        {
            HUD.isActive = (HUD.requiredMode == activeMode);
        }
    }
}