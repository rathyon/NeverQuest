using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : Trap {
	void Start(){
		_damage = 0.05f;
		placementX = 0.5f;
        placementY = -1.3f;
		trapName = "Poison Trap";
		cost = 50;
		_stopTime = 5.0f;
		description = "Strange gas that deals damage to 0.5 damage per frame";
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            var mob = collision.GetComponent<MobController>();
            mob.slowPercentage = 0.8f;
            mob.HP -= _damage;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            var mob = collision.GetComponent<MobController>();
            mob.slowPercentage = 1f;

        }
    }

}
