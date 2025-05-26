using UnityEngine;

public class Player : MonoBehaviour
{
    public SO_PlayerMotionSettings settings;    
    private Velocimeter2D velocimeter;

    private void Start()
    {
        velocimeter = gameObject.AddComponent<Velocimeter2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.None) == false)
            MovePlayer();
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxis(settings.axisNameX);
        var verticalInput = Input.GetAxis(settings.axisNameY);

        if (horizontalInput != 0 && Mathf.Abs(velocimeter.measurement.y) <= settings.thresholdToGoHorizontal)
        {
            transform.Translate(Time.deltaTime * settings.speedX * horizontalInput * Vector3.right);
        }
        
        if (verticalInput != 0 && Mathf.Abs(velocimeter.measurement.x) <= settings.thresholdToGoVertical)
        {
            transform.Translate(Time.deltaTime * settings.speedY * verticalInput * Vector3.up);
        }
    }
}
