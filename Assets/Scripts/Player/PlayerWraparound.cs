using UnityEngine;

public class PlayerWraparound : MonoBehaviour
{
    public GameObject playerCounterpart;
    public string screenEdgeTag;
    public float screenWidth;

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) < Mathf.Abs(collision.transform.position.x))
        {
            playerCounterpart.SetActive(false);
        }
    }

    private void OnDisable()
    {
        counterpartFreedom.SetDependenceStatus(false);
    }

    private void TranslateCounterpart()
    {
        var position = TransposePosition(transform.position, screenWidth);
        var rotation = transform.rotation;

        playerCounterpart.transform.SetPositionAndRotation(position, rotation);
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
