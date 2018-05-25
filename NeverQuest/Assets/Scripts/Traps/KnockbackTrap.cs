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
		description = "If it worked it would push enemies back a bit";
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
        if (collision.gameObject.CompareTag("Mob")){
            print("entrei");
			if (active) {
                print("e again");
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = (transform.forward * 200);
				 
			}
		}
	}

}

