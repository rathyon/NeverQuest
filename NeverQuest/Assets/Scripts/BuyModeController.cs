using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyModeController : MonoBehaviour {

    public GameObject testTrap;

	public GameObject canTRAP;
	public GameObject cannotTRAP;

    public GameObject player;

	GameObject can;
	GameObject cannot;


	private Vector3 colorRed = new Vector3(1,0,0);
	private Vector3 colorGreen = new Vector3(0,1,0);

	public float offset = 3;

    private bool invalidPlacement;
	public PlayerController player_script; 

	// Use this for initialization
	void Start () {
		can = Instantiate (canTRAP, transform.position, Quaternion.identity);
		can.SetActive (false);
		cannot = Instantiate (cannotTRAP, transform.position, Quaternion.identity);
		cannot.SetActive (false);

        invalidPlacement = false;
		player_script = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player_script.buymodeActive) {
			
			if (player_script.facingright) {
				transform.position = new Vector3 (player.transform.position.x + offset, transform.position.y, transform.position.z);
			} 
			else {
				transform.position = new Vector3 (player.transform.position.x - offset, transform.position.y, transform.position.z);
			}
			if (!invalidPlacement) {
				cannot.SetActive (false);
				can.transform.position = transform.position;
				can.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) {
	            
					if (player_script.gold > 10) {
						Instantiate (testTrap, transform.position, Quaternion.identity);
						player_script.gold -= 10;
					}
				}
			} else {
				can.SetActive (false);
				cannot.transform.position = transform.position;
				cannot.SetActive (true);	
			}
		} 
		else {
			cannot.SetActive (false);
			can.SetActive (false);
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrapOnce"))
        {
            invalidPlacement = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TrapOnce"))
        {
            invalidPlacement = false;
        }
    }
}
