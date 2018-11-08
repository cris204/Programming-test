using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private static PlayerMovement instance;
    public static PlayerMovement Instance
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

    private float deltaSpeed;

    [SerializeField]
    private Vector3 aimDirection = Vector2.zero;

    [Header("Components")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Joystick joystick;

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
    void Start () {
        vectorSpeed = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {

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
        if (rb.velocity.normalized != Vector3.zero)
        {
            anim.SetFloat("Speed", vectorSpeed.magnitude);
        }
    }

    private void CalculateDirections()
    {
        if (Mathf.Abs(vectorSpeed.x) >= 0.2)
        {
            aimDirection.x = vectorSpeed.x;
        }
        if (Mathf.Abs(vectorSpeed.y) >= 0.2)
        {
            aimDirection.z = vectorSpeed.y;
        }
        transform.rotation=Quaternion.LookRotation(aimDirection.normalized);

    }
    private void AsignInputs()
    {
        vectorSpeed.x = (joystick.Horizontal * (speed * Time.deltaTime));
        vectorSpeed.y = (joystick.Vertical * (speed * Time.deltaTime));
    }


}
