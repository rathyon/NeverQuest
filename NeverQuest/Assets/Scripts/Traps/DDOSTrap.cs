using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOSTrap : Trap {

	private float inactiveTimerMAX;
	private float  waitedTime;

	void Start(){
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "DDOS Trap";
		cost = 15;
		_stopTime = 20.0f;
		inactiveTimerMAX = 4.0f;
		waitedTime = 0.0f;
		_damage = 0;
		description = "Placing it calls on a hacker that stuns whoever passes through it, just 20s, otherwise it would be op";
	}

	void Update(){
		if (!active) {
			waitedTime += Time.deltaTime;
			if (waitedTime >= inactiveTimerMAX) {
				waitedTime = 0.0f;
				active = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.slowTimerMAX = _stopTime;
			mob.slowPercentage = 0.0f;
			mob.slowed = true;
			active = false;

		}
	}

}
