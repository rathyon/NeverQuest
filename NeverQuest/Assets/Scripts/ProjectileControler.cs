using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControler : MonoBehaviour {
	private float speed;
	private float _damage;
	private int moveHorizontal;
	private float lifeTime;
	// Use this for initialization
	void Start () {
		speed = 5.0f;
		_damage = 10;
		bool facingRight = GameObject.Find("Player").GetComponent<PlayerController>().facingRight;
		transform.position = GameObject.Find ("Player").transform.position;
		if (facingRight) {moveHorizontal = 1;
		} else {moveHorizontal = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		transform.position += movement * speed * 0.1f;
		lifeTime += Time.deltaTime;
		if (lifeTime >= 3) {
			Destroy (gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Mob")){
			var mob =collision.GetComponent<MobController> ();
			mob.HP -= _damage;
			Destroy (gameObject);

		}
	}
}

