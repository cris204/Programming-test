using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private bool haveKey;

    [SerializeField]
    private GameObject haveKeyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivatedNeedAKey(bool activated)
    {
        haveKeyText.SetActive(activated);
    }

    #region Get&Set

    public bool HaveKey
    {
        get
        {
            return haveKey;
        }

        set
        {
            haveKey = value;
        }
    }

    #endregion
}
