using UnityEngine;

public class PlayerHealth : HealthManager
{
    public override void OnDeath()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        GetComponent<HingeJoint2D>().enabled = false;
    }
}
