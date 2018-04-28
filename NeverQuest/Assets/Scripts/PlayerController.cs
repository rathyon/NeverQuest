using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    //public bool grounded = true;
    public float jumpPower;

    private bool facingRight = true;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;
    public GameObject player;

    // Use this for initialization
    void Start () {
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

    // Update is called once per frame
    void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        if (Input.GetKeyDown(KeyCode.Space))
            //if(grounded)
                GetComponent<Rigidbody2D>().velocity = new Vector2(movement.x, jumpPower);

    }


}
