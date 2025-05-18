using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DalekMover : MonoBehaviour
{
    public bool isFlipped { get; private set; }

    public float horizontalSpeed;
    public List<string> obstacleTags;

    private void Awake()
    {
        isFlipped = false;
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
        transform.rotation = Quaternion.Euler(new Vector3(0, -transform.eulerAngles.y + 180, 0));
        isFlipped = !isFlipped;
    }
}
