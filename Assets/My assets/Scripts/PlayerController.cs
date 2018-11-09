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

    [Header("General")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 vectorSpeed;
    [SerializeField]
    private Vector2 vectorRotation;
    private float deltaSpeed;

    [SerializeField]
    private Vector3 aimDirection = Vector2.zero;

    [Header("Components")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Joystick leftjoystick;
    [SerializeField]
    private Joystick rightjoystick;

    [Header("Shoot")]
    [SerializeField]
    private JoyButton joyButton;
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
        vectorSpeed = new Vector2(0, 0);
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
        if (!Shooting.Instance.CanShot || PlayerController.Instance.JoyButton.pressed)
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
        if (Mathf.Abs(vectorRotation.x) >= 0.2)
        {
            aimDirection.x = vectorRotation.x;
        }
        if (Mathf.Abs(vectorRotation.y) >= 0.2)
        {
            aimDirection.z = vectorRotation.y;
        }
        transform.rotation = Quaternion.LookRotation(AimDirection.normalized);

    }

    private void AsignInputs()
    {
        vectorSpeed.x = (leftjoystick.Horizontal * (speed * Time.deltaTime));
        vectorSpeed.y = (leftjoystick.Vertical * (speed * Time.deltaTime));

        vectorRotation.x = (rightjoystick.Horizontal * (speed * Time.deltaTime));
        vectorRotation.y = (rightjoystick.Vertical * (speed * Time.deltaTime));
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
            other.GetComponent<Animator>().SetBool("Near", true);
        }
        if (other.CompareTag("Inside"))
        {
            other.GetComponent<SpriteRenderer>().enabled=(false);
        }
        if (other.CompareTag("DoorInside"))
        {
            other.transform.parent.GetComponent<SpriteRenderer>().enabled = (false);
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
    }


    #endregion

    #region Get&Set
    public JoyButton JoyButton
    {
        get
        {
            return joyButton;
        }

        set
        {
            joyButton = value;
        }
    }

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

    #endregion
}
