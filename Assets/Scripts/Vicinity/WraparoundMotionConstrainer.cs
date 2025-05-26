using System.Collections;
using UnityEngine;

public class WraparoundMotionConstrainer : MonoBehaviour
{ 
    private Coroutine constrainerRoutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player) && CheckSimultaneousInputs(player))
        {
            constrainerRoutine = StartCoroutine(ConstrainVelocity(player));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player) && CheckSimultaneousInputs(player) && constrainerRoutine == null)
        {
            constrainerRoutine = StartCoroutine(ConstrainVelocity(player));
        }
    }

    private IEnumerator ConstrainVelocity(Player player)
    {
        var speedY = player.settings.speedY;
        player.settings.ChangeSpeed(0, -speedY);

        yield return new WaitWhile(() => CheckSimultaneousInputs(player));
        player.settings.ChangeSpeed(0, +speedY);

        constrainerRoutine = null;
    }

    private bool CheckSimultaneousInputs(Player player)
    {
        if (player == null)
            return false;

        return Input.GetAxisRaw(player.settings.axisNameX) != 0 && Input.GetAxisRaw(player.settings.axisNameY) != 0;
    }
}
