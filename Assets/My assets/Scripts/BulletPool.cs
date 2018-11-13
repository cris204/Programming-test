using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{

    private static BulletPool instance;

    public static BulletPool Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Rigidbody bulletPrefab;
    [SerializeField]
    private int size;
    [SerializeField]
    private List<Rigidbody> bullets;
    private Rigidbody bullet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrepareBullets();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PrepareBullets()
    {
        bullets = new List<Rigidbody>();
        for (int i = 0; i < size; i++)
        {
            AddBullet();
        }
    }

    public Rigidbody GetBullet()
    {
        if (bullets.Count == 0)
        {
            AddBullet();
        }
        return AllocateBullet();
    }

    public void ReleaseBullet(Rigidbody bullet)
    {
        bullet.velocity = (Vector3.zero);
        bullet.gameObject.SetActive(false);

        bullets.Add(bullet);

    }

    private void AddBullet()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.gameObject.SetActive(false);
        bullets.Add(bullet);
    }

    private Rigidbody AllocateBullet()
    {
        bullet = bullets[bullets.Count - 1];
        bullets.RemoveAt(bullets.Count - 1);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}