using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    private static Shooting instance;
    public static Shooting Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Shoot")]
    [SerializeField]
    private float timeToShot;
    private WaitForSeconds waitToShot;
    [SerializeField]
    private bool canShot;
    [Header("General")]
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float force;
    private Rigidbody bullet;
    Vector3 aimDirection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // waitToShot = timeToShot;
        canShot = true;
    }

    public void Attack()
    {
        if (PlayerController.Instance.JoyButton.pressed && canShot)
        {
            bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = weapon.transform.position;
            // bullet.velocity = transform.up * force;
            weapon.transform.rotation=Quaternion.LookRotation(aimDirection);
            bullet.velocity = weapon.transform.up * force;
            canShot = false;
            StartCoroutine(Shot());
        }


    }

    public IEnumerator Shot()
    {
        yield return new WaitForSeconds(timeToShot);
        canShot = true;
    }
}
