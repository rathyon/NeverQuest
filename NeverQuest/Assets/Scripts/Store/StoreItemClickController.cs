using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class StoreItemClickController : Selectable {

	public int type;

	public GameObject objectSelected;

	public Button yourButton;
	private GameObject placement;
	BaseEventData m_BaseEvent;

	private bool canUpdate;

	void Start()
	{
		canUpdate = true;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		if (GameObject.Find ("Player").GetComponent<PlayerController> ().storeActive) {
			if (type == 1) {
				Trap trap = objectSelected.GetComponent<Trap> ();
				placement = GameObject.FindGameObjectWithTag ("Placement");
				placement.GetComponent<TrapPlacement> ().setObject (trap);
				placement.GetComponent<TrapPlacement> ().placement = true;
				GetComponentInParent<PlayerController> ().activateStore ();
			}
		}

	}



	void Update()
	{
		//Check if the GameObject is being highlighted
		if (IsHighlighted (m_BaseEvent) == true) {
//			Debug.Log (yourButton.colors);
//			ColorBlock colorVar = yourButton.colors;
//			colorVar.highlightedColor = new Color (255, 116, 0);
//			yourButton.colors = colorVar;
			if (canUpdate) {
				Vector3 x = GameObject.Find ("Trap_Description").transform.localScale;
				x.x = 1;
				GameObject.Find ("Trap_Description").transform.localScale = x;
				x.x = 0;
				GameObject.Find ("WA_Description").transform.localScale = x;
				Trap trap = objectSelected.GetComponent<Trap> ();
				GameObject.Find ("Description_Name").GetComponent<Text> ().text = trap.trapName;
				GameObject.Find ("Description_Image").GetComponent<Image> ().sprite = objectSelected.GetComponent<SpriteRenderer> ().sprite;
				GameObject.Find ("Description_Damage_Text").GetComponent<Text> ().text = trap._damage.ToString();
				GameObject.Find ("Description_Text").GetComponent<Text> ().text = trap.description;
			}
			canUpdate = false;
		} else {
			if (!canUpdate) {
				canUpdate = true;
			}
		}
	}
}
