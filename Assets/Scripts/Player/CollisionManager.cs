using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public string obstacleTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            GetComponent<HingeJoint2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            GetComponent<HingeJoint2D>().enabled = true;
        }
    }
}
