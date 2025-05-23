using UnityEngine;

public class Velocimeter2D : MonoBehaviour
{
    public Vector2 measurement;
    private Vector3 previousPosition;

    private void Awake()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        UpdateValues();
    }

    private Vector3 CalculateVelocity()
    {
        var deltaDistances = transform.position - previousPosition;
        var instantVelocity = deltaDistances / Time.deltaTime;

        return instantVelocity;
    }

    private void UpdateValues()
    {
        measurement = CalculateVelocity();
        previousPosition = transform.position;
    }
}
