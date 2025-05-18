using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public string enemyTag = "Enemy";

    [Header("Attack Settings")]
    public Transform heightMeasurementOrigin;
    public float fallOnEnemyGravity;
    public float minHeightToDamageEnemy;
    public string groundBlockTag;
    public float damageDealt;

    [Header("Attack Input")]
    public KeyCode keyToAttack = KeyCode.Space;

    private Rigidbody2D player;
    private float measuredHeight = 0;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        damageDealt = Mathf.Abs(damageDealt);
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(heightMeasurementOrigin.position, Vector3.down);

        if (Input.GetKeyDown(keyToAttack))
        {
            if (hit.collider.gameObject.CompareTag(groundBlockTag))
            {
                measuredHeight = hit.distance;
            }

            player.gravityScale = fallOnEnemyGravity;
            GetComponent<HingeJoint2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;

        if (obj.CompareTag(enemyTag) && measuredHeight >= minHeightToDamageEnemy)
        {
            obj.GetComponent<HealthManager>().UpdateHealth(-damageDealt);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundBlockTag))
        {
            measuredHeight = 0;
            player.gravityScale = 1;
        }
    }
}
