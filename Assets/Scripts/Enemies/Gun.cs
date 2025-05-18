using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string playerTag = "Player";

    [Header("Gun Settings")]
    public float damageAmount;
    public float initialAngleOfAim;
    public float angleBetweenShots;
    public float intervalBetweenShots;
    public int shotsPerCycle;

    private DalekMover parent;
    private LineRenderer lineRenderer;
    private Coroutine rotationCoroutine;
    private Coroutine damageControlCoroutine;
    private float currentAngle;
    private bool canDamage;

    private void Awake()
    {
        damageAmount = Mathf.Abs(damageAmount);
        parent = GetComponentInParent<DalekMover>();
        lineRenderer = GetComponent<LineRenderer>();
        currentAngle = initialAngleOfAim;
        canDamage = true;
    }

    private void Start()
    {
        rotationCoroutine = StartCoroutine(RotateAngleOfAim());
        damageControlCoroutine = StartCoroutine(ControlDamageDealt());
    }

    private void Update()
    {
        var angle = Mathf.Deg2Rad * currentAngle;
        var sign = GetOrientationX(parent.isFlipped);
        var dir = new Vector2(sign * Mathf.Cos(angle), Mathf.Sin(angle));
        var hit = Physics2D.Raycast(transform.position, dir);

        DrawShot(hit);
        DealDamage(hit);
    }

    private void OnDisable()
    {
        StopCoroutine(rotationCoroutine);
        StopCoroutine(damageControlCoroutine);
    }

    private void DrawShot(RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.point);
    }

    private void DealDamage(RaycastHit2D hit)
    {
        var obj = hit.collider.gameObject;

        if (obj != null && obj.CompareTag(playerTag) && canDamage)
        {
            obj.GetComponent<HealthManager>().UpdateHealth(-damageAmount);
            canDamage = false;
        }
    }

    private int GetOrientationX(bool isFlipped)
    {
        if (isFlipped)
            return -1;
        else
            return +1;
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

    private IEnumerator ControlDamageDealt()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalBetweenShots);
            canDamage = true;
        }
    }
}
