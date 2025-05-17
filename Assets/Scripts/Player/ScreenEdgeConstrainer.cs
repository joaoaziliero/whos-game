using UnityEngine;

public class ScreenEdgeConstrainer : MonoBehaviour
{
    private Player player;
    private float originalSpeedY;
    private string screenEdgeTag;

    private void Awake()
    {
        player = GetComponent<Player>();
        originalSpeedY = player.speedY;
        screenEdgeTag = GetComponent<PlayerWraparound>().screenEdgeTag;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(screenEdgeTag))
        {
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");

            player.speedY =
                inputX != 0 && inputY != 0 ? 0 :
                originalSpeedY;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(screenEdgeTag))
        {
            player.speedY = originalSpeedY;
        }
    }
}
