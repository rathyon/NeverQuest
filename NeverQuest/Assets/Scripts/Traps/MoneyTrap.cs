using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTrap : Trap {
	void Start(){
        active = true;
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "Money bait";
		cost = 15;
		_stopTime = 2.0f;
		description = "Place some coins around a see those peasants pick it up for 5s";
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob") && active){
			var mob =collision.GetComponent<MobController> ();
			mob.slowTimerMAX = _stopTime;
			mob.slowPercentage = 0.0f;
			mob.slowed = true;
            active = false;
            Destroy(gameObject, 2);
		}
	}

}
