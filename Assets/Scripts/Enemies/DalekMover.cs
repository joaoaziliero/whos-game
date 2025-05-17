using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DalekMover : MonoBehaviour
{
    public float horizontalSpeed;
    public List<string> obstacleTags;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obstacleDetected = obstacleTags.Any(tag => collision.gameObject.CompareTag(tag));

        if (obstacleDetected)
            FlipMovementDirection();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * horizontalSpeed * Vector3.right);
    }

    private void FlipMovementDirection()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        horizontalSpeed = -horizontalSpeed;
    }
}
