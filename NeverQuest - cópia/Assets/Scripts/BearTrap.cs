using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : Trap {

	public BearTrap(){
		name = "Bear Trap";
		cost = 20;
		_damage = 50;
		_stopTime = 1.0f;
		slowPercentage = 0.3f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Mob")){

			var mob =collision.GetComponent<MobControler> ();
			mob.HP -= _damage;
			mob.slowed = true;
			mob.slowTimerMAX = 5.0f;
			mob.speedSlowPercentage = slowPercentage;
			mob.speed *= slowPercentage;
		}
	}

}
