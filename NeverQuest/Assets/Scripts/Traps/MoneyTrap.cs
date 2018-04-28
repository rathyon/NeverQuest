using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTrap : Trap {
	void Start(){
		trapName = "Money bait";
		cost = 15;
		_stopTime = 5.0f;
	}

	//private void OnTriggerEnter2D(Collider2D collision){
	//	if (collision.gameObject.CompareTag("Mob")){
	//		var mob =collision.GetComponent<GameObject> ();
	//		mob.slowTimerMAX = _stopTime;
	//		mob.slowed = true;

	//	}
	//}

}
