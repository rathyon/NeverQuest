using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOSTrap : Trap {

	private float inactiveTimerMAX;
	private float  waitedTime;

	void Start(){
		placementX = 0.5f;
		placementY = -1.5f;
		trapName = "DDOS Trap";
		cost = 55;
		_stopTime = 10.0f;
		inactiveTimerMAX = 4.0f;
		waitedTime = 0.0f;
		_damage = 0;
		description = "Placing it calls on a hacker that stuns whoever passes through it, just 20s, otherwise it would be op";
	}

	void Update(){
		
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.slowTimerMAX = _stopTime;
			mob.slowPercentage = 0.0f;
			mob.slowed = true;
			active = false;
            Destroy(gameObject);
		}
	}

}
