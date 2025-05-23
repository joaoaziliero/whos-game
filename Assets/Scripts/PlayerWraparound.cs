using UnityEngine;

public class PlayerWraparound : MonoBehaviour
{
    public string screenEdgeTag;
    public GameObject playerCounterpart;

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
            //counterpartFreedom.FreezeBody();
            //counterpartFreedom.AllowControl(false);

            var pos = Vector2.zero;
            pos.x = transform.position.x - Mathf.Sign(transform.position.x) * 17.8f;
            pos.y = transform.position.y;
            playerCounterpart.transform.position = pos;

            playerCounterpart.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            counterpartFreedom.FreezeRotation();
            playerCounterpart.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(screenEdgeTag) && selfFreedom.canBodyRotate)
        {
            playerCounterpart.transform.position = new Vector2(transform.position.x - Mathf.Sign(transform.position.x) * 17.8f, transform.position.y);
            playerCounterpart.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            if (Mathf.Abs(transform.position.x) - 0.5f >= Mathf.Abs(collision.transform.position.x))
            {
                counterpartFreedom.Unfreeze();
                selfFreedom.canBodyRotate = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.transform;

        if (obj.CompareTag(screenEdgeTag) && Mathf.Abs(transform.position.x) > Mathf.Abs(obj.position.x))
        {
            //counterpartFreedom.UnfreezeBody();
            //counterpartFreedom.AllowControl(true);
            gameObject.SetActive(false);
        }
    }
}
