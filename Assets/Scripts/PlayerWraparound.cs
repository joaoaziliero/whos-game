using UnityEngine;

public class PlayerWraparound : MonoBehaviour
{
    public string screenEdgeTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.transform;
        var pos = transform.position;
        var count = CountPlayerInstances();

        if (obj.CompareTag(screenEdgeTag) && Mathf.Abs(pos.x) < Mathf.Abs(obj.position.x) && count == 1)
        {
            var offset = 2 * Mathf.Sign(pos.x) * Mathf.Abs(obj.position.x - pos.x);
            pos.x = (-1) * (pos.x + offset);

            var playerClone = Instantiate(gameObject);
            playerClone.transform.position = pos;

#if UNITY_EDITOR
            playerClone.name = gameObject.name;
#endif
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.transform;
        var pos = transform.position;
        var count = CountPlayerInstances();

        if (obj.CompareTag(screenEdgeTag) && Mathf.Abs(pos.x) > Mathf.Abs(obj.position.x) && count > 1)
        {
            Destroy(gameObject);
        }
    }

    private int CountPlayerInstances()
    {
        return GameObject.FindGameObjectsWithTag(transform.tag).Length;
    }
}
