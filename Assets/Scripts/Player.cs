using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Reference")]
    public Rigidbody2D player;

    [Header("Player Settings")]
    public float speed;
    public float tiltForce;
    public float tiltOffset;
    public Vector2 centerOfMass;

    [Header("Keyboard Input")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    private void Awake()
    {
        AdjustCenterOfMass(centerOfMass);
    }

    private void Update()
    {
        var sign = GetHorizontalMotion();

        if (sign != 0)
        {
            var force = sign * tiltForce * Vector2.right;
            var point = transform.position + tiltOffset * Vector3.up;

            OnMoveTiltPlayer(force, point);
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!Input.GetKey(up) && !Input.GetKey(down))
        {
            transform.Translate(Time.deltaTime * speed * Input.GetAxis("Horizontal") * Vector3.right);
        }
        else if (!Input.GetKey(left) && !Input.GetKey(right))
        {
            transform.Translate(Time.deltaTime * speed * Input.GetAxis("Vertical") * Vector3.up);
        }
    }

    private void OnMoveTiltPlayer(Vector2 tiltForce, Vector3 tiltPoint)
    {
        player.AddForceAtPosition(tiltForce, tiltPoint, ForceMode2D.Impulse);
    }

    private void AdjustCenterOfMass(Vector2 centerPoint)
    {
        player.centerOfMass = centerPoint;
    }

    private int GetHorizontalMotion()
    {
        if (Input.GetKeyDown(left))
            return -1;
        else if (Input.GetKeyDown(right))
            return +1;
        else
            return +0;
    }
}
