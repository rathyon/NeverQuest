using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapController : MonoBehaviour {

	public int damage;
	public bool wait;
	public float waittimeIron;

	private float waitedTime = 0.0f;


	// Use this for initialization
	void Start () {
		gameObject.name = gameObject.GetInstanceID ().ToString ();
	}

	// Update is called once per frame
	void Update () {
		if (wait) {
			waitedTime += Time.deltaTime;
			if (waitedTime >= waittimeIron) {
				waitedTime = 0.0f;
				wait = false;
			}
		}
	}
	public void removeTrap(){
		Destroy (gameObject);
	}

}
