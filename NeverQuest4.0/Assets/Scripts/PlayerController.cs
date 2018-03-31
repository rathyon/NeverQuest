using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Rigidbody2D rg2d;

	private bool facingright = true;
	SpriteRenderer _spriteRenderer;

	void Awake(){
		rg2d = GetComponent<Rigidbody2D> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		movement ();
	}

	void movement(){
		float movHorizontal = Input.GetAxis ("Horizontal");
		float movVertical = 0;//Input.GetAxis ("Vertical");
		Vector3 move =new Vector3(movHorizontal, movVertical, 0);
		if (_spriteRenderer != null) {
			if (movHorizontal < 0 && facingright) {
				facingright = !facingright;
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
			if (movHorizontal > 0 && !facingright) {
				facingright = !facingright;
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
		}

		transform.position += move * speed * Time.deltaTime;
	}
}
