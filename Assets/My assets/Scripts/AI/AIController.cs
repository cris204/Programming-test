using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent pathManager;
    public Transform t;


	// Use this for initialization
	void Start () {
        pathManager = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        pathManager.SetDestination(t.position);
	}
}
