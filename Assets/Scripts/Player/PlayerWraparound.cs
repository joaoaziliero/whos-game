using UnityEngine;

public class PlayerWraparound : MonoBehaviour
{
    public GameObject playerCounterpart;
    public float counterpartFreeingThreshold;
    public float screenWidth;
    public string screenEdgeTag;

    private FreedomDegreesManager counterpartFreedom;

    private void Awake()
    {
        counterpartFreedom = playerCounterpart.GetComponent<FreedomDegreesManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) < Mathf.Abs(collision.transform.position.x))
        {
            TranslateCounterpart();
            counterpartFreedom.SetDependenceStatus(true);
            playerCounterpart.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(screenEdgeTag) && counterpartFreedom.isTransformDependent)
        {
            TranslateCounterpart();
            SetCounterpartFree(collision.transform, counterpartFreeingThreshold);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) > Mathf.Abs(collision.transform.position.x) && playerCounterpart.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void TranslateCounterpart()
    {
        var position = TransposePosition(transform.position, screenWidth);
        var rotation = transform.rotation;

        playerCounterpart.transform.SetPositionAndRotation(position, rotation);
    }

    private void SetCounterpartFree(Transform screenEdge, float threshold)
    {
        if (Mathf.Abs(transform.position.x) - threshold >= Mathf.Abs(screenEdge.position.x))
        {
            counterpartFreedom.SetDependenceStatus(false);
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
