using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemStoreController : MonoBehaviour {

	public GameObject item;
	private Trap trap;
	private string nameT;
	private string cost;
	private SpriteRenderer image;
	private Text[] slotTexts;

	//// Use this for initialization
	void Start () {
		trap = item.GetComponent<Trap> ();
		nameT = trap.trapName;
		cost = trap.cost.ToString();
		slotTexts = gameObject.GetComponentsInChildren<Text> ();

		slotTexts[0].text = nameT; 
		slotTexts[1].text = cost; 
		gameObject.GetComponentInChildren<Image> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
	}

	//void Start () {
	//	trap = item.GetComponent<Trap> ();
	//	nameT = trap.trapName;
	//	cost = trap.cost.ToString();
	//	slotTexts = gameObject.GetComponentsInChildren<Text> ();
	
	//	slotTexts[0].text = nameT; 
	//	slotTexts[1].text = cost; 
	//	gameObject.GetComponentInChildren<Image> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
	//}

}