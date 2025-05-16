using System.Collections;
using UnityEngine;

public class FrictionManager : MonoBehaviour
{
    public string playerTag = "Player";

    [Header("Friction Settings")]
    public float horizontalSpeedDifference;
    public float verticalSpeedDifference;

    private void Awake()
    {
        horizontalSpeedDifference = Mathf.Abs(horizontalSpeedDifference);
        verticalSpeedDifference = Mathf.Abs(verticalSpeedDifference);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            var player = collision.gameObject.GetComponent<Player>();

            player.speedX -= horizontalSpeedDifference;
            player.speedY -= verticalSpeedDifference;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            var player = collision.gameObject.GetComponent<Player>();

            player.speedX += horizontalSpeedDifference;
            player.speedY += verticalSpeedDifference;
        }
    }
}
