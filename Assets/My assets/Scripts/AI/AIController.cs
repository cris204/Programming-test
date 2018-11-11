using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    private NavMeshAgent pathManager;
    [SerializeField]
    private Transform target;
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
    public Transform apuntar;

    // Use this for initialization
    void Start () {
        pathManager = GetComponent<NavMeshAgent>();
        pathManager.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetPostition.x = target.position.x;
        targetPostition.y = target.transform.position.y;
        targetPostition.z= apuntar.position.z;

        apuntar.transform.LookAt(targetPostition);

        correctRot.x = target.transform.position.x;
        correctRot.y = apuntar.transform.position.y;
        correctRot.z = target.transform.position.z;

        model.transform.LookAt(correctRot);

        if (detectPlayer)
        {
            pathManager.SetDestination(target.transform.position);
            if (Input.GetKeyDown(KeyCode.A))
            {
                pathManager.isStopped = true;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                pathManager.isStopped = false;
            }
        }
    }



    #region Collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            detectPlayer = true;
            StartCoroutine("Shooting");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
