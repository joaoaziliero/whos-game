using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speedX;
    public float thresholdToGoVertical;
    public float speedY;
    public float thresholdToGoHorizontal;

    private Velocimeter2D velocimeter;

    private void OnEnable()
    {
        velocimeter = gameObject.AddComponent<Velocimeter2D>();
    }

    private void Update()
    {
        // Para evitar cálculos desnecessários no método MovePlayer()
        // caso as teclas de movimento estejam inativas:
        if (Input.GetKey(KeyCode.None) == false)
            MovePlayer();
    }

    private void OnDisable()
    {
        Destroy(velocimeter);
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 && Mathf.Abs(velocimeter.measurement.y) <= thresholdToGoHorizontal)
        {
            transform.Translate(Time.deltaTime * speedX * horizontalInput * Vector3.right);
        }
        
        if (verticalInput != 0 && Mathf.Abs(velocimeter.measurement.x) <= thresholdToGoVertical)
        {
            transform.Translate(Time.deltaTime * speedY * verticalInput * Vector3.up);
        }
    }
}
