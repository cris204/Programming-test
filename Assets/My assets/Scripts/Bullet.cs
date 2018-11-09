using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
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