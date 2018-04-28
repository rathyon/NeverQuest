using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    //public bool grounded = true;
    public float jumpPower;

	public int gold;
    public bool facingRight = true;

    SpriteRenderer spriteRenderer;
<<<<<<< HEAD
    Rigidbody2D rb2d;
    public GameObject player;

    // Use this for initialization
    void Start () {
=======
    Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
		gold = 200;
		gameObject.GetComponentInChildren<Canvas> ().enabled = false;
>>>>>>> 7a27fc6a0a788fcf51000c0a61a9a7076815e714
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        if (spriteRenderer != null)
        {
            if (moveHorizontal < 0 && facingRight)
            {
                facingRight = !facingRight;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            if (moveHorizontal > 0 && !facingRight)
            {
                facingRight = !facingRight;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

            //transform.position += movement * speed * 0.1f;
            rb2d.AddForce(movement * speed);

    }
	public void activateStore(){
		gameObject.GetComponentInChildren<Canvas> ().enabled = !gameObject.GetComponentInChildren<Canvas> ().enabled;
	}
    // Update is called once per frame
<<<<<<< HEAD
    void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        if (Input.GetKeyDown(KeyCode.Space))
            //if(grounded)
                GetComponent<Rigidbody2D>().velocity = new Vector2(movement.x, jumpPower);

    }


=======
    void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			activateStore ();
		}
	}
>>>>>>> 7a27fc6a0a788fcf51000c0a61a9a7076815e714
}
