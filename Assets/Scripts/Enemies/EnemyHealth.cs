using UnityEngine;

public class EnemyHealth : HealthManager
{
    public float destroyAfterSeconds;

    public override void OnDeath()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        GetComponentInChildren<LineRenderer>().enabled = false;
        Destroy(gameObject, destroyAfterSeconds);
    }
}
