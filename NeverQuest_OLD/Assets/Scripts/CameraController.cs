using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    /*
     * This camera always follows the player
     */

    public GameObject player;

    private Vector3 zOffset = new Vector3(0.0f, 0.0f, -10.0f);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = player.transform.position + zOffset;
	}
}
