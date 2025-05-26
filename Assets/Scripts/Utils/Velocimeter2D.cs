using UnityEngine;

public class Velocimeter2D : MonoBehaviour
{
    public string motionAxisNameX = "Horizontal";
    public string motionAxisNameY = "Vertical";
    public Vector2 measurement = Vector2.zero;

    private Vector3 previousPosition;

    private void Start()
    {
        UpdatePosition();
    }

    private void Update()
    {
        if (Input.GetAxis(motionAxisNameX) != 0 || Input.GetAxis(motionAxisNameY) != 0)
        {
            UpdateMeasurement();
            UpdatePosition();
        }
        else if (previousPosition != transform.position)
        {
            UpdatePosition();
        }
        else if (measurement != Vector2.zero)
        {
            ClearMeasurement();
        }
    }

    private void OnDisable()
    {
        ClearMeasurement();
    }

    private Vector3 CalculateVelocity()
    {
        var deltaDistances = transform.position - previousPosition;
        var instantVelocity = deltaDistances / Time.deltaTime;

        return instantVelocity;
    }

    private void ClearMeasurement()
    {
        measurement = Vector2.zero;
    }

    private void UpdateMeasurement()
    {
        measurement = CalculateVelocity();
    }

    private void UpdatePosition()
    {
        previousPosition = transform.position;
    }
}
