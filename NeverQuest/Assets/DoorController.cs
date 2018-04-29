using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject linkedDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.R) && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().transform.position = linkedDoor.transform.position;
        }

    }

}
