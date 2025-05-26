using UnityEngine;

public class FrictionManager : MonoBehaviour
{
    [Header("Friction Settings")]
    public float horizontalSpeedDifference;
    public float verticalSpeedDifference;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            player.settings.ChangeSpeed(-horizontalSpeedDifference, -verticalSpeedDifference);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            player.settings.ChangeSpeed(+horizontalSpeedDifference, +verticalSpeedDifference);
        }
    }
}
