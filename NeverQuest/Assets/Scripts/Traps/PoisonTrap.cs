using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : Trap {
	void Start(){
		_damage = 0.5f;
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "Poisonous Floor";
		cost = 65;
		_stopTime = 5.0f;
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;

		}
	}

}
