using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : Trap {
	void Start(){
		_damage = 0.5f;
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "Poison Trap";
		cost = 65;
		_stopTime = 5.0f;
		description = "Strange gas that deals damage to 0.5 damage per frame";
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;

		}
	}

}
