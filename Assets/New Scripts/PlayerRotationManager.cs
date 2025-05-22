using UnityEngine;

public class PlayerRotationManager : MonoBehaviour
{
    public Transform rotationHelper;
    public Transform altimeterHelper;

    private bool canSetRotation = true;

    private void Update()
    {
        if (canSetRotation)
            SetRotation();
    }

    private void SetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotationHelper.rotation.eulerAngles.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {

        }
    }
}
