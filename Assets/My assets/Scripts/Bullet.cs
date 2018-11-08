using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBody;
    private void Awake()
    {

        rigidBody = GetComponent<Rigidbody>();
    }



    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReleaseBullet(rigidBody);
    }
}