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
        CanShot = true;
    }

    public void Attack()//organizar
    {
        //weapon.transform.rotation = Quaternion.LookRotation( aimDirection,Vector3.up);
            weapon.transform.rotation = Quaternion.LookRotation(new Vector3(PlayerController.Instance.AimDirection.x, PlayerController.Instance.AimDirection.z, PlayerController.Instance.AimDirection.y));
        if (PlayerController.Instance.JoyButton.pressed && CanShot)
        {
            bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = weapon.transform.position;
            bullet.velocity = weapon.transform.forward * force;
            CanShot = false;
            StartCoroutine(Shot());
            
        }

    }

    public IEnumerator Shot()
    {
        yield return new WaitForSeconds(timeToShot);
        CanShot = true;
    }

    #region Get&Set
    public bool CanShot
    {
        get
        {
            return canShot;
        }

        set
        {
            canShot = value;
        }
    }
    #endregion
}
