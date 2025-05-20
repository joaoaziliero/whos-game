using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float healthPoints;
    public float damageFeedbackDuration;
    public Color damageFeedbackColor;
    public Color baselineColor;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashColorRoutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = baselineColor;
        flashColorRoutine = null;
    }

    private void Update()
    {
        if (healthPoints <= 0)
            OnDeath();
    }

    private void OnDisable()
    {
        if (flashColorRoutine != null)
            StopCoroutine(flashColorRoutine);
    }

    public void UpdateHealth(float amount)
    {
        healthPoints += amount;

        if (amount < 0)
            OnDamage();
    }

    public virtual void OnDamage()
    {
        if (flashColorRoutine == null)
        {
            flashColorRoutine = StartCoroutine(FlashColor(spriteRenderer));
        }
    }

    public virtual void OnDeath()
    {

    }

    private IEnumerator FlashColor(SpriteRenderer renderer)
    {
        var originalColor = renderer.color;
        var colorDiff = damageFeedbackColor - originalColor;
        var halfTotalTime = damageFeedbackDuration / 2;
        var t = 0.0f;

        while (t < halfTotalTime)
        {
            renderer.color += colorDiff * (t / halfTotalTime);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (t > 0)
        {
            renderer.color -= colorDiff * (1 - (t / halfTotalTime));
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        renderer.color = originalColor;
        flashColorRoutine = null;
    }
}
