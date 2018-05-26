using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTrap : Trap {
	private float inactiveTimerMAX;
	private float  waitedTime;

	void Start(){
		active = true;
		placementX = 0.5f;
		placementY = -0.7f;
		trapName = "Knockback Trap";
		cost = 45;
		inactiveTimerMAX = 2.5f;
		waitedTime = 0.0f;
		description = "We do not garantee that it will work! Use at your own risk!";
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


	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") && active){
            print("entrei");
			if (active) {
                if (collision.gameObject.GetComponent<MobController>().facingRight) { collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-35f, 20f); }
                else { collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(35f, 20f); }
                active = false;
				 
			}
		}
	}

}

