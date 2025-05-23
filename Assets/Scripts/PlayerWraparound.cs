using UnityEngine;

public class PlayerWraparound : MonoBehaviour
{
    public GameObject playerCounterpart;
    public float counterpartFreeingThreshold;
    public float screenWidth;
    public string screenEdgeTag;

    private FreedomDegreesManager selfFreedom;
    private FreedomDegreesManager counterpartFreedom;

    private void Awake()
    {
        selfFreedom = GetComponent<FreedomDegreesManager>();
        counterpartFreedom = playerCounterpart.GetComponent<FreedomDegreesManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.transform;

        if (obj.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) < Mathf.Abs(obj.position.x))
        {
            CauseTransformImitation();
            counterpartFreedom.FreezeBody();

            selfFreedom.SetMasterStatus(true);
            counterpartFreedom.SetMasterStatus(false);

            playerCounterpart.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.transform;

        if (obj.CompareTag(screenEdgeTag) && selfFreedom.isMasterObject)
        {
            CauseTransformImitation();
            SetCounterpartFree(obj, counterpartFreeingThreshold);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.transform;

        if (obj.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) > Mathf.Abs(obj.position.x))
        {
            gameObject.SetActive(false);
        }
    }

    private void CauseTransformImitation()
    {
        var position = TransposePosition(transform.position, screenWidth);
        var rotation = transform.rotation;

        playerCounterpart.transform.SetPositionAndRotation(position, rotation);
    }

    private void SetCounterpartFree(Transform screenEdge, float threshold)
    {
        if (Mathf.Abs(transform.position.x) - threshold >= Mathf.Abs(screenEdge.position.x))
        {
            counterpartFreedom.Unfreeze();
            selfFreedom.SetMasterStatus(false);
        }
    }

    private Vector2 TransposePosition(Vector2 position, float horizontalDistance)
    {
        return new Vector2()
        {
            x = position.x - Mathf.Sign(position.x) * horizontalDistance,
            y = position.y,
        };
    }
}
