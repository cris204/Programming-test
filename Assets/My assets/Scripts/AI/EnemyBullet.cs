using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float damage;


    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void OnBecameInvisible()
    {
        BulletPool.Instance.ReleaseBullet(rb);
    }

    private void OnTriggerEnter(Collider other)
    {
        BulletEnemyPool.Instance.ReleaseBullet(rb);
    }



    #region Get&Set
    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }
    #endregion
}