using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : Trap {

	void Start(){
		placementX = 0.5f;
        placementY = -1.2f;
		trapName = "Bear Trap";
		cost = 35;
		_damage = 50.0f;
		_stopTime = 1.0f;
		slowPercentage = 0.3f;
		description = "If you place it legend says they get slowed by 30%, but that might be a myth";
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Mob")){

			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;
			mob.slowed = true;
			mob.slowTimerMAX = 5.0f;
			mob.slowPercentage = slowPercentage;
            Destroy(gameObject);
		}
	}

}
