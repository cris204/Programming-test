using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }
    [Header("Stats")]
    [SerializeField]
    private float health;

    [Header("General")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 vectorSpeed;
    [SerializeField]
    private Vector2 vectorRotation;
    private float deltaSpeed;
    [SerializeField]
    private float timeToRotate;
    [SerializeField]
    private bool rightJoystickToRotate;
    [SerializeField]
    private Rigidbody rb;



    [Header("Components and shoot")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Vector3 aimDirection = Vector2.zero;
    [SerializeField]
    private Joystick leftjoystick;
    [SerializeField]
    private Joystick rightjoystick;
    [SerializeField]
    public Vector3 bulletDirection;

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
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        vectorSpeed = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        AsignInputs();
        AsignAnimation();
        CalculateDirections();
    }

    private void FixedUpdate()
    {

        rb.velocity = vectorSpeed;
    }

    private void AsignAnimation()
    {
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            anim.SetFloat("Speed", vectorSpeed.magnitude);
        }
        if (!Shooting.Instance.CanShot || Rightjoystick.Direction.magnitude>0.8f)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    private void CalculateDirections()
    {
        if ( vectorRotation.x!=0 || vectorRotation.x != 0)
        {
            if (Mathf.Abs(vectorRotation.x) >= 0.2)
            {
                aimDirection.x = vectorRotation.x;
            }
            if (Mathf.Abs(vectorRotation.y) >= 0.2)
            {
                aimDirection.z = vectorRotation.y;
            }
            transform.rotation = Quaternion.LookRotation(AimDirection.normalized);
            timeToRotate = 0;
        }
        else if(timeToRotate>=2)
        {
            if (Mathf.Abs(vectorSpeed.x) >= 0.2)
            {
                aimDirection.x = vectorSpeed.x;
            }
            if (Mathf.Abs(vectorSpeed.y) >= 0.2)
            {
                aimDirection.z = vectorSpeed.y;
            }
            transform.rotation = Quaternion.LookRotation(AimDirection.normalized);

        }
        timeToRotate += Time.deltaTime;
    }

    private void AsignInputs()
    {
        vectorSpeed.x = (Leftjoystick.Horizontal * (speed * Time.deltaTime));
        vectorSpeed.y = (Leftjoystick.Vertical * (speed * Time.deltaTime));

        vectorRotation.x = (Rightjoystick.Horizontal * (speed * Time.deltaTime));
        vectorRotation.y = (Rightjoystick.Vertical * (speed * Time.deltaTime));
    }

    public void Shoot()
    {
        Shooting.Instance.Attack();
    }


    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (GameManager.Instance.HaveKey)
            {
                other.GetComponent<Animator>().SetBool("Near", true);
            }
        }
        if (other.CompareTag("Inside"))
        {
            other.GetComponent<SpriteRenderer>().enabled=(false);
        }
        if (other.CompareTag("DoorInside"))
        {
            other.transform.parent.GetComponent<SpriteRenderer>().enabled = (false);
        }
        if (other.CompareTag("Key"))
        {
            GameManager.Instance.HaveKey = true;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("LockDoor"))
        {
            if (GameManager.Instance.HaveKey)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                GameManager.Instance.ActivatedNeedAKey(true);
            }
            
        }

        if (other.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.ModifyHealthBar();
            health -= other.GetComponent<EnemyBullet>().Damage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            other.GetComponent<Animator>().SetBool("Near", false);
        }
        if (other.CompareTag("Inside"))
        {
            other.GetComponent<SpriteRenderer>().enabled = (true);
        }
        if (other.CompareTag("DoorInside"))
        {
            other.transform.parent.GetComponent<SpriteRenderer>().enabled = (true);
        }
        if (other.CompareTag("LockDoor"))
        {

            GameManager.Instance.ActivatedNeedAKey(false);

        }
    }


    #endregion

    #region Get&Set

    public Vector3 AimDirection
    {
        get
        {
            return aimDirection;
        }

        set
        {
            aimDirection = value;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public Joystick Leftjoystick
    {
        get
        {
            return leftjoystick;
        }

        set
        {
            leftjoystick = value;
        }
    }

    public Joystick Rightjoystick
    {
        get
        {
            return rightjoystick;
        }

        set
        {
            rightjoystick = value;
        }
    }
    #endregion
}
