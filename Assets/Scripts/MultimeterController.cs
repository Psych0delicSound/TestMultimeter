using Unity.VisualScripting;
using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private MultimeterModel model;
    [SerializeField] private Regulator regulator;

    private void Start()
    {
        regulator.Rotated += model.SetActiveMode;
    }


}
