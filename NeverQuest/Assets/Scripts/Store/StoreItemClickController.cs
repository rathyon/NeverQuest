using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class StoreItemClickController : MonoBehaviour {

	public int type;

	public GameObject objectSelected;

	public Button yourButton;
	private GameObject placement;

	void Start()
	{
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
}
