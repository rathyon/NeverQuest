using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngQHability : MonoBehaviour {
	private float timeOfLife;
	public float damagePerFrame;
	private int moveHorizontal;
	private bool lastChange;
	private float lifeTime;
    public float maxLifeTime;
	private GameObject playerCont;
	// Use this for initialization
	void Start () {
		lastChange = true;
        damagePerFrame = 1;
		playerCont = GameObject.Find ("Player");
		timeOfLife = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
        //if (!playerCont.GetComponent<PlayerController>().flamethrowerOn)
        //{
        //    playerCont.GetComponent<PlayerController>().flamethrowerOn = false;
        //    playerCont.GetComponent<PlayerController>().canFlamethrower = true;
        //    print("DEU MERDA");
        //    Destroy(gameObject);
        //}
        timeOfLife += Time.deltaTime;  
		if (timeOfLife >= maxLifeTime || !playerCont.GetComponent<PlayerController>().flamethrowerOn) {
			playerCont.GetComponent<PlayerController> ().flamethrowerOn = false;
            playerCont.GetComponent<PlayerController>().canFlamethrower = true;
            Destroy (gameObject);
		}


		if (!playerCont.GetComponent<PlayerController> ().facingRight) {
			Vector3 position = new Vector3 (playerCont.transform.position.x - 2.0f,playerCont.transform.position.y +0.4f,playerCont.transform.position.z);

			transform.position = position;
		} else {
			Vector3 position = new Vector3 (playerCont.transform.position.x + 2.0f,playerCont.transform.position.y +0.4f,playerCont.transform.position.z);

			transform.position = position;
		}
		if (lastChange!=playerCont.GetComponent<PlayerController> ().facingRight) {
			lastChange = playerCont.GetComponent<PlayerController> ().facingRight;
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x *= -1;
			gameObject.transform.localScale = newScale;
		}
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= damagePerFrame;

		}
	}
}

