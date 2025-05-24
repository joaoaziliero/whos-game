using UnityEngine;

public class HingeJointManager : MonoBehaviour
{
    public string obstacleTag;
    private HingeJoint2D hinge;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            SetHingeActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            SetHingeActive(true);
        }
    }

    public void SetHingeActive(bool value)
    {
        hinge.enabled = value;
    }
}
