using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponsANDArmor : MonoBehaviour {
	public int type;//0-armor 1-weapon
	private bool active;

	public Button yourButton;

	private PlayerController playerControler;

	void Start()
	{
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
}
