using System;
using UnityEngine;

public class Regulator : MonoBehaviour
{
    [SerializeField] GameObject regulatorObj;
    [SerializeField] GameObject highlighter;
    [SerializeField] private float scrollStregth = 8000;
    
    public bool isActive { get; private set; }

    public event Action<float> Rotated;

    private void Awake()
    {
        highlighter.SetActive(false);
    }

    private void Update()
    {
        if (!isActive) return;

        Rotate(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void OnMouseEnter()
    {
        isActive = true;
        highlighter.SetActive(true);
    }

    private void OnMouseExit()
    {
        isActive = false;
        highlighter.SetActive(false);
    }

    private void Rotate(float inputValue)
    {
        Transform transform = regulatorObj.transform;

        Vector3 newEulerAngle = new (
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            transform.eulerAngles.z + inputValue * scrollStregth * Time.deltaTime);
            
        transform.eulerAngles = newEulerAngle;

        float normalizedAngle = Mathf.DeltaAngle(transform.eulerAngles.z, 0f) * -1f;

        Rotated?.Invoke(normalizedAngle);
    }
}
