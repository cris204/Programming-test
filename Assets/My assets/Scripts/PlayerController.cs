using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private JoyButton joyButton;

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }



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

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        Shoot();
	}

    public void Shoot()
    {
        Shooting.Instance.Attack();
    }

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
    #endregion
}
