using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EenemyBullet : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }


    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReleaseBullet(rb);
    }
}