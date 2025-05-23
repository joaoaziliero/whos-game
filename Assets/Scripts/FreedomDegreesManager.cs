using UnityEngine;

public class FreedomDegreesManager : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public bool isMasterObject;

    public void FreezeBody()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Unfreeze()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;
    }

    public void SetMasterStatus(bool status)
    {
        isMasterObject = status;
    }
}
