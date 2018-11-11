using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float damage;

    private Rigidbody rb;
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
        BulletPool.Instance.ReleaseBullet(rb);
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