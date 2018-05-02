using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacement : MonoBehaviour {

	public GameObject canTRAP;
	public GameObject cannotTRAP;
	public GameObject player;

	Trap trap;

	GameObject can;
	GameObject cannot;

	public bool placement;

	private bool invalidPlacement;
	private PlayerController player_script; 

	// Use this for initialization
	void Start () {
		placement = false;
		can = Instantiate (canTRAP, transform.position, Quaternion.identity);
		can.SetActive (false);
		cannot = Instantiate (cannotTRAP, transform.position, Quaternion.identity);
		cannot.SetActive (false);

		invalidPlacement = false;
		player_script = gameObject.GetComponentInParent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {

		if (placement) {
			Vector3 position;
			if (player_script.facingRight) {
				position = new Vector3 (player.transform.position.x + 2, player.transform.position.y + trap.placementY, transform.position.z);
			} 
			else {
				position = new Vector3 (player.transform.position.x - 2, player.transform.position.y + trap.placementY, transform.position.z);
			}
			transform.position = position;
			if (!invalidPlacement) {
				cannot.SetActive (false);
				can.transform.position = position;
				can.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) {
					
					if (player_script.gold >= trap.cost) {
							Instantiate (trap, position, Quaternion.identity);
						player_script.gold -= trap.cost;
						}
				}
			} else {
				can.SetActive (false);
				cannot.transform.position = position;
				cannot.SetActive (true);	
			}
		} 
		else {
			cannot.SetActive (false);
			can.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			placement = false;
		}
	}

	public void setObject(Trap trapObject){
		trap = trapObject ;
		cannot.GetComponent<SpriteRenderer> ().sprite = can.GetComponent<SpriteRenderer> ().sprite = trap.GetComponent<SpriteRenderer> ().sprite;

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Trap"))
		{
			invalidPlacement = true;

		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Trap"))	{
			invalidPlacement = false;
		}
	}
}
