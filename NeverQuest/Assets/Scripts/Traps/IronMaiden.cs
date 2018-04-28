using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMaiden : Trap {
	private float inactiveTimerMAX;
	private float  waitedTime;
	
	void Start(){
		trapName = "Iron Maiden";
		cost = 75;
		active = true;
		_damage = 150;
		inactiveTimerMAX = 1.5f;
		waitedTime = 0.0f;
	
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
	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (collision.gameObject.CompareTag("Mob")){
	//		var mob =collision.GetComponent<MobControler> ();
	//		mob.HP -= _damage;
	//		active = false;

	//	}
	//}

}
