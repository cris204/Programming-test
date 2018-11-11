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
    [SerializeField]
    private Vector3 rotationVector;
    [Header("General")]
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float force;
    private Rigidbody bullet;
    private Vector3 aimDirection;
    [SerializeField]
    private float offset;

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
        rotationVector = new Vector3(35.262f, -2.06f, 22.851f);

        // waitToShot = timeToShot;
        CanShot = true;
    }

    public void Attack()//organizar
    {
        //weapon.transform.rotation = Quaternion.LookRotation( aimDirection,Vector3.up);
        rotationVector.x = PlayerController.Instance.AimDirection.x + offset;
        rotationVector.y = PlayerController.Instance.AimDirection.z + offset;
        rotationVector.z = PlayerController.Instance.AimDirection.y;
        weapon.transform.rotation = Quaternion.LookRotation(rotationVector);

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
