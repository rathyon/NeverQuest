using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapController : MonoBehaviour {

    public int damage;


	// Use this for initialization
	void Start () {
		gameObject.name = gameObject.GetInstanceID ().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void removeTrap(){
		Destroy (gameObject);
	}

}
