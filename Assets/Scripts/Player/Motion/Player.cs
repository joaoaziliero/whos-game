using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speedX;
    public float changeToVerticalAxisThreshold;
    public float speedY;
    public float changeToHorizontalAxisThreshold;
    public float tiltForce;
    public float tiltOffset;
    public Vector2 centerOfMass;

    [Header("Horizontal Axis Keys")]
    public KeyCode left;
    public KeyCode right;

    private Rigidbody2D player;
    private HingeJoint2D joint;
    private Velocimeter2D velocimeter;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.centerOfMass = centerOfMass;
        joint = GetComponent<HingeJoint2D>();
    }

    private void Start()
    {
        velocimeter = GetComponent<Velocimeter2D>() == null ?
            gameObject.AddComponent<Velocimeter2D>():
            gameObject.GetComponent<Velocimeter2D>();
    }

    private void Update()
    {
        var sign = GetHorizontalOrientation();

        if (sign != 0 && joint.enabled)
        {
            var force = sign * tiltForce * Vector2.right;
            var point = transform.position + tiltOffset * Vector3.up;

            OnMoveTiltPlayer(force, point);
        }

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

    private void OnMoveTiltPlayer(Vector2 tiltForce, Vector3 tiltPoint)
    {
        player.AddForceAtPosition(tiltForce, tiltPoint, ForceMode2D.Impulse);
    }

    private int GetHorizontalOrientation()
    {
        if (Input.GetKeyDown(left))
            return -1;
        if (Input.GetKeyDown(right))
            return +1;

        return 0;
    }
}
