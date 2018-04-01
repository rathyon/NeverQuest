using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyModeController : MonoBehaviour {

    public GameObject testTrap;
    public GameObject player;

    private bool invalidPlacement;

	// Use this for initialization
	void Start () {
        invalidPlacement = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var player_script = player.GetComponent<PlayerController>();
            if (!invalidPlacement && player_script.gold > 10)
            {
                Instantiate(testTrap, transform.position, Quaternion.identity);
                player_script.gold -= 10;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            invalidPlacement = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            invalidPlacement = false;
        }
    }
}
