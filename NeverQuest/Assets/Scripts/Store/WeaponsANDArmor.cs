using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class WeaponsANDArmor : Selectable {
	public int type;//0-armor 1-weapon
	private bool active;

	public Button yourButton;

	private PlayerController playerControler;

	private bool canUpdate;

	BaseEventData m_BaseEvent;

	void Start()
	{
		canUpdate = true;
		playerControler = GetComponentInParent<PlayerController> ();
		active = true;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (active) {
			if (type == 1) {
				if (playerControler.gold >= 100) {
					playerControler.bullet_damage = 15.0f;
					playerControler.gold -= 100;
					active = false;
				}
			}
			if (type == 3) {
				if (playerControler.gold >= 250) {
					playerControler.bullet_damage = 25.0f;
					playerControler.gold -= 250;
					active = false;
				}
			}

			if (type == 0) {
				if (playerControler.gold >= 50) {
					playerControler.timeAccept = 6f;
					playerControler.gold -= 50;
					active = false;
				}
			}
			if (type == 2) {
				if (playerControler.gold >= 200) {
					playerControler.timeAccept = 7f;
					playerControler.gold -= 200;
					active = false;
				}
			}
		}
	}

		void Update()
		{
			//Check if the GameObject is being highlighted
			if (IsHighlighted (m_BaseEvent) == true) {
				if (canUpdate) {
				Vector3 x = GameObject.Find ("Trap_Description").transform.localScale;
				x.x = 0;
				GameObject.Find ("Trap_Description").transform.localScale = x;
				x.x = 1;
				GameObject.Find ("WA_Description").transform.localScale = x;
				if (type == 0) {
					GameObject.Find ("Description_Name_WA").GetComponent<Text> ().text = "Leather Armor";
					GameObject.Find ("Description_Text_WA").GetComponent<Text> ().text = "Increases the time of quest acceptance, but no one really knows why, it just does";
					GameObject.Find ("Description_Damage_Text_WA").GetComponent<Text> ().text = "Time increase to 6";
				}
				if (type == 1) {
					GameObject.Find ("Description_Name_WA").GetComponent<Text> ().text = "Rusty Shotgun";
					GameObject.Find ("Description_Text_WA").GetComponent<Text> ().text = "Old shotgun the owner probably already died, carefull not to cut yourself, you might catch gangrene";
					GameObject.Find ("Description_Damage_Text_WA").GetComponent<Text> ().text = "Damage increased to 15";

				}
				if (type == 2) {
					GameObject.Find ("Description_Name_WA").GetComponent<Text> ().text = "Steel Armor";
					GameObject.Find ("Description_Text_WA").GetComponent<Text> ().text = "Increases the time of quest acceptance, but no one really knows why, it just does";
					GameObject.Find ("Description_Damage_Text_WA").GetComponent<Text> ().text = "Time increase to 7";
				}
				if (type == 3) {
					GameObject.Find ("Description_Name_WA").GetComponent<Text> ().text = "Golden Shotgun";
					GameObject.Find ("Description_Text_WA").GetComponent<Text> ().text = "The mother of all shotguns, a product definitely made by a dwarf, or a gnome...well anyone with the expertise";
					GameObject.Find ("Description_Damage_Text_WA").GetComponent<Text> ().text = "Damage increased to 25";
				}
					
				}
				canUpdate = false;
			} else {
				if (!canUpdate) {
					canUpdate = true;
				}
			}
		}
	}
	
