using UnityEngine;

public class FreedomDegreesManager : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Player player;
    public bool canBodyRotate;

    public void FreezeBody()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        canBodyRotate = false;
    }

    public void FreezeRotation()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        canBodyRotate = false;
    }

    public void Unfreeze()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;
        canBodyRotate = true;
    }

    public void AllowControl(bool state)
    {
        player.enabled = state;
    }
}
