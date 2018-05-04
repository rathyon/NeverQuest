using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class StoreTabSelector : Selectable {
	public int type;

	BaseEventData m_BaseEvent;

	private bool canUpdate;


	public void Start()
	{
		canUpdate = true;
	}
	void Update()
	{
		/*if (IsHighlighted (m_BaseEvent) == true) {
			if (canUpdate) {
				if (type == 1) {
					Vector3 x = GameObject.Find ("Traps").transform.localScale;
						x.x = 1;
						GameObject.Find ("Traps").transform.localScale = x;
						x.x = 0;
					GameObject.Find ("Weapons_Armor").transform.localScale = x;
					GameObject.Find ("Weapons_Armor").SetActive (false);
					GameObject.Find ("Traps").SetActive (true);
				}
				else if (type == 2) {
					Vector3 x = GameObject.Find ("Weapons_Armor").transform.localScale;
					x.x = 1;
					GameObject.Find ("Weapons_Armor").transform.localScale = x;
					x.x = 0;
					GameObject.Find ("Traps").transform.localScale = x;

					GameObject.Find ("Weapons_Armor").SetActive (true);
					GameObject.Find ("Traps").SetActive (false);
				} 
		
			}
			canUpdate = false;
		} else {
			if (!canUpdate) {
				canUpdate = true;
			}
		}*/

	}

}
