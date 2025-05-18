using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthManager
{
    public override void OnDeath()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<LineRenderer>().enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        Destroy(gameObject, 5);
    }
}
