using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    private NavMeshAgent agent;
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
    [SerializeField]
    private float dist;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource audioIA;
    [SerializeField]
    private AudioClip[] clips;
    

    // Use this for initialization
    void Start () {
        GameManager.Instance.EnemyNumber++;
        GameManager.Instance.EnemiesLeftText();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        target = lastTarget;
        audioIA = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = agent.remainingDistance;
        targetPostition.x = target.position.x;
        targetPostition.y = target.transform.position.y;
        targetPostition.z = apuntar.position.z;

        apuntar.transform.LookAt(targetPostition);
        correctRot.x = apuntar.transform.localEulerAngles.y + 90;
        correctRot.y = -90;
        correctRot.z = -90;
        model.transform.localEulerAngles = correctRot;

        agent.SetDestination(target.transform.position);

        if (!detectPlayer)
        {
            if (dist <= 1)
            {
                target = path[Random.Range(0, path.Length)];
            }
        }
        else
        {
            if (dist<3f)
            {
                agent.speed = 0;
               
            }
            else
            {
                agent.speed = 2;
            }
        }
        anim.SetFloat("Speed", agent.speed);
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
            audioIA.clip = clips[0];
            
            target = other.transform;
            detectPlayer = true;
            anim.SetLayerWeight(1, 1);
            StartCoroutine("Shooting");
        }
        if (other.CompareTag("Bullet"))
        {
            health -= other.GetComponent<Bullet>().Damage;
            if (health <= 0)
            {
                GameManager.Instance.EnemyNumber--;
                GameManager.Instance.EnemiesLeftText();
                audioIA.clip = clips[1];
                audioIA.Play();
                gameObject.SetActive(false);
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
            agent.speed = 1;
            anim.SetLayerWeight(1, 0);
        }
    }

    #endregion


    #region coroutine

    IEnumerator Shooting()
    {
        while (detectPlayer)
        {
            yield return null;
            audioIA.clip = clips[0];
            audioIA.Play();
            bullet = BulletEnemyPool.Instance.GetBullet();
            bullet.transform.localPosition = weapon.transform.position;
            bullet.transform.localRotation = apuntar.transform.rotation;
            bullet.velocity = apuntar.transform.forward * force;
            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion


}
