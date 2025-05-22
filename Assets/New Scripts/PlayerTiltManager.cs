using UnityEngine;

public class PlayerTiltManager : MonoBehaviour
{
    [Header("Tilt Settings")]
    public Vector2 centerOfMass;
    public float tiltForce;
    public float tiltOffset;

    [Header("Tilt Keys")]
    public KeyCode left;
    public KeyCode right;

    private Rigidbody2D player;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.centerOfMass = centerOfMass;
    }

    private void Update()
    {
        var sign = GetHorizontalMotion();

        if (sign != 0)
        {
            var force = sign * tiltForce * Vector2.right;
            var point = transform.position + tiltOffset * Vector3.up;

            TiltPlayer(force, point);
        }
    }

    private void TiltPlayer(Vector2 tiltForce, Vector3 tiltPoint)
    {
        player.AddForceAtPosition(tiltForce, tiltPoint, ForceMode2D.Impulse);
    }

    private int GetHorizontalMotion()
    {
        if (Input.GetKeyDown(left))
            return -1;
        if (Input.GetKeyDown(right))
            return +1;
        
        return 0;
    }
}
