﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMaiden : Trap {
	private float inactiveTimerMAX;
	private float  waitedTime;
	
	void Start(){
		placementX = 0.3f;
		placementY = 0.5f;
		trapName = "Iron Maiden";
		cost = 75;
		active = true;
		_damage = 150.0f;
		inactiveTimerMAX = 1.5f;
		waitedTime = 7.0f;
		description = "The makers of the game thought this was too op so it has a cd of 7s";
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
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;
			active = false;

		}
	}

}
