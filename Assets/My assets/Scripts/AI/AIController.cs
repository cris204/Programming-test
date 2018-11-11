using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    private NavMeshAgent navMesh;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform lastTarget;
    [SerializeField]
    private Transform[] path;
    [SerializeField]
    private bool detectPlayer;
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private float force;
    [SerializeField]
    private Vector3 correctRot;
    [SerializeField]
    private Vector3 targetPostition;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private Transform apuntar;
    [SerializeField]
    private float health;
    [SerializeField]
    private int destination;
    

    // Use this for initialization
    void Start () {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = false;
        target = lastTarget;
    }

    // Update is called once per frame
    void Update()
    {

        targetPostition.x = target.position.x;
        targetPostition.y = target.transform.position.y;
        targetPostition.z = apuntar.position.z;

        apuntar.transform.LookAt(targetPostition);
        correctRot.x = apuntar.transform.localEulerAngles.y + 90;
        correctRot.y = -90;
        correctRot.z = -90;
        model.transform.localEulerAngles = correctRot;

        navMesh.SetDestination(target.transform.position);

        if (navMesh.velocity.sqrMagnitude <= 0.1f)//organizar final
        {
            if (destination < path.Length-1)
            {
                destination++;
                target = path[destination];
            }
            else
            {
                destination=0;
                target = path[destination];
            }
        }

    }



    #region Collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (target.tag != "Player")
            {
                lastTarget = target;
            }

            target = other.transform;
            detectPlayer = true;
            StartCoroutine("Shooting");
        }
        if (other.CompareTag("Bullet"))
        {
            health -= other.GetComponent<Bullet>().Damage;
            if (health <= 0)
            {
                GameManager.Instance.EnemyNumber--;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            target = (lastTarget);

            detectPlayer = false;
            StopCoroutine("Shooting");
        }
    }

    #endregion


    #region coroutine

    IEnumerator Shooting()
    {
        while (detectPlayer)
        {
            yield return null;
            bullet = BulletEnemyPool.Instance.GetBullet();
            bullet.transform.localPosition = model.transform.position;
            bullet.velocity = weapon.transform.forward * force;
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion


}
