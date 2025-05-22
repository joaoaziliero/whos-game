using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speedX;
    public float changeToVerticalAxisThreshold;
    public float speedY;
    public float changeToHorizontalAxisThreshold;

    private Velocimeter2D velocimeter;

    private void Start()
    {
        velocimeter = GetComponent<Velocimeter2D>() == null ?
            gameObject.AddComponent<Velocimeter2D>():
            gameObject.GetComponent<Velocimeter2D>();
    }

    private void Update()
    {
        // Para evitar cálculos desnecessários no método MovePlayer()
        // caso as teclas de movimento estejam inativas:
        if (Input.GetKey(KeyCode.None) == false)
            MovePlayer();
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 && Mathf.Abs(velocimeter.measurement.y) <= changeToHorizontalAxisThreshold)
        {
            transform.Translate(Time.deltaTime * speedX * horizontalInput * Vector3.right);
        }
        
        if (verticalInput != 0 && Mathf.Abs(velocimeter.measurement.x) <= changeToVerticalAxisThreshold)
        {
            transform.Translate(Time.deltaTime * speedY * verticalInput * Vector3.up);
        }
    }
}
