using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

	private bool facingright = true;
    //private Rigidbody2D rb2d;

	//SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
		//_spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		/*if (_spriteRenderer != null) {
			if (moveHorizontal < 0 && facingright) {
				facingright = !facingright;
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
			if (moveHorizontal > 0 && !facingright) {
				facingright = !facingright;
				_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
		}*/

        transform.position += movement * speed * 0.1f;
	}
}
