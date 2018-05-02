using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngQHability : MonoBehaviour {
	private float timeOfLife;
	private float _damage;
	private int moveHorizontal;
	private bool lastChange;
	private float lifeTime;
	private GameObject playerCont;
	// Use this for initialization
	void Start () {
		lastChange = true;
		_damage = 1;
		playerCont = GameObject.Find ("Player");
		timeOfLife = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (!playerCont.GetComponent<PlayerController> ().qHabilityFlag) {
			Destroy (gameObject);
		}
		timeOfLife += Time.deltaTime;  
		if (timeOfLife >= 7) {
			playerCont.GetComponent<PlayerController> ().qHabilityFlag = false;
			Destroy (gameObject);
		}


		if (!playerCont.GetComponent<PlayerController> ().facingRight) {
			Vector3 position = new Vector3 (playerCont.transform.position.x - 2.8f,playerCont.transform.position.y +0.4f,playerCont.transform.position.z);

			transform.position = position;
		} else {
			Vector3 position = new Vector3 (playerCont.transform.position.x + 2.8f,playerCont.transform.position.y +0.4f,playerCont.transform.position.z);

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
			mob.HP -= _damage;

		}
	}
}

