using UnityEngine;

public class Velocimeter2D : MonoBehaviour
{
    public string motionAxisNameX = "Horizontal";
    public string motionAxisNameY = "Vertical";
    public Vector2 measurement = Vector2.zero;

    private Vector3 previousPosition;

    private void Update()
    {
        if (Input.GetAxis(motionAxisNameX) != 0 || Input.GetAxis(motionAxisNameY) != 0)
        {
            UpdateValues();
        }
        else if (previousPosition != transform.position)
        {
            previousPosition = transform.position;
        }
        else if (measurement != Vector2.zero)
        {
            measurement = Vector2.zero;
        }
    }

    private void OnDisable()
    {
        measurement = Vector2.zero;
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
