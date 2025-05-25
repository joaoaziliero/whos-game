using System.Collections;
using UnityEngine;

public class WraparoundMotionConstrainer : MonoBehaviour
{
    private Player player;
    private Coroutine constrainerRoutine;
    private float originalSpeedY;
    private string screenEdgeTag;

    private void Awake()
    {
        player = GetComponent<Player>();
        screenEdgeTag = GetComponent<PlayerWraparound>().screenEdgeTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(screenEdgeTag))
        {
            constrainerRoutine = StartCoroutine(ConstrainVelocity());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(screenEdgeTag) && AreSimultaneousInputs() && constrainerRoutine == null)
        {
            constrainerRoutine = StartCoroutine(ConstrainVelocity());
        }
    }

    private void OnDisable()
    {
        if (player.settings.speedY < originalSpeedY)
        {
            player.settings.ChangeSpeed(0, originalSpeedY);
        }
    }

    private IEnumerator ConstrainVelocity()
    {
        originalSpeedY = player.settings.speedY;
        player.settings.ChangeSpeed(0, -originalSpeedY);

        yield return new WaitWhile(() => AreSimultaneousInputs());
        player.settings.ChangeSpeed(0, +originalSpeedY);

        constrainerRoutine = null;
    }

    private bool AreSimultaneousInputs()
    {
        return Input.GetAxisRaw(player.settings.axisNameX) != 0 && Input.GetAxisRaw(player.settings.axisNameY) != 0;
    }
}
