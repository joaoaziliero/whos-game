using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float initialAngleOfAim;
    public float angleBetweenShots;
    public float intervalBetweenShots;
    public int shotsPerCycle;

    private DalekMover parent;
    private LineRenderer lineRenderer;
    private Coroutine rotationCoroutine;
    private float currentAngle;

    private void Awake()
    {
        parent = GetComponentInParent<DalekMover>();
        lineRenderer = GetComponent<LineRenderer>();
        currentAngle = initialAngleOfAim;
    }

    private void Start()
    {
        rotationCoroutine = StartCoroutine(RotateAngleOfAim());
    }

    private void Update()
    {
        var angle = Mathf.Deg2Rad * currentAngle;
        var sign = GetOrientationX(parent.isFlipped);
        var dir = new Vector2(sign * Mathf.Cos(angle), Mathf.Sin(angle));
        var hit = Physics2D.Raycast(transform.position, dir);

        DrawShots(hit);
    }

    private void OnDestroy()
    {
        StopCoroutine(rotationCoroutine);
    }

    private IEnumerator RotateAngleOfAim()
    {
        var counter = 0;

        while (true)
        {
            yield return new WaitForSeconds(intervalBetweenShots);
            currentAngle += angleBetweenShots;
            counter++;

            if (counter == shotsPerCycle)
            {
                currentAngle = initialAngleOfAim;
                counter = 0;
            }
        }
    }

    private void DrawShots(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }

    private int GetOrientationX(bool isFlipped)
    {
        if (isFlipped)
            return -1;
        else
            return +1;
    }
}
