using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float healthPoints;
    public float damageFeedbackDuration;
    public Color damageFeedbackColor;

    private void Update()
    {
        if (healthPoints <= 0)
            OnDeath();
    }

    public void UpdateHealth(float amount)
    {
        healthPoints += amount;

        if (amount < 0)
            OnDamage();
    }

    public virtual void OnDamage()
    {
        StartCoroutine(FlashColor(GetComponent<SpriteRenderer>()));
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
            renderer.color = originalColor + colorDiff * (t / halfTotalTime);
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
    }
}
