//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody2D>().centerOfMass = Vector2.down;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody2D>().AddForceAtPosition(2 * Vector2.right, transform.position + 0.5f * Vector3.up, ForceMode2D.Impulse);
        //transform.DOMoveX(15.0f, 5.0f).SetRelative(true).SetEase(Ease.InOutSine);
    }

    void Update()
    {

    }
}
