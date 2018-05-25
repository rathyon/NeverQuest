using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap {
	void Start(){
		_damage = 0.2f;
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "Fire Trap";
		cost = 75;
		description = "Burn them all! Just place it near water so nothing bad happens, oh and deals damage per frame";
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;

		}
	}

}
