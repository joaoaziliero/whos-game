using UnityEngine;

public class PlayerTiltControl : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float tiltForce;
    public float tiltHeight;
    public Vector2 playerCenterOfMass;

    [Header("Horizontal Axis Keys")]
    public KeyCode left;
    public KeyCode right;

    private Rigidbody2D rigidBody;
    private HingeJoint2D hinge;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = playerCenterOfMass;
        hinge = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        var sign = GetHorizontalMotion();
        
        if (sign != 0 && hinge.enabled)
        {
            var force = sign * tiltForce * Vector2.right;
            var point = transform.position + tiltHeight * Vector3.up;

            TiltPlayer(force, point);
        }
    }

    private void TiltPlayer(Vector2 tiltForce, Vector3 tiltPoint)
    {
        rigidBody.AddForceAtPosition(tiltForce, tiltPoint, ForceMode2D.Impulse);
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
