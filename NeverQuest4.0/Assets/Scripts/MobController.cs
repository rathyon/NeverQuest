using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour {

	private float health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (health <= 90) {
			Destroy (gameObject);
		}
	}

	void OnTriggerStay(Collider other){
		//traps like spikes, deal dmage once
		if (other.gameObject.CompareTag ("traponce")) {
			var scripother = other.GetComponent<TrapOnce> ();
			if (scripother != null) {
				health -= scripother.damage ();
			}
		}
		//traps that deal damage over time
		/*if (other.gameObject.CompareTag ("traptime")) {
			health -= other.GetComponent(trap).damage () * Time.deltaTime;
		}*/
	}
}
