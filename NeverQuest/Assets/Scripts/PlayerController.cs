using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    //public bool grounded = true;
    public float jumpPower;

    public int gold;
    public bool facingRight = true;
    public GameObject player;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
		gold = 200;
		gameObject.GetComponentInChildren<Canvas> ().enabled = false;
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

        int margem_lateral = 1;

        //Wall1
        if ((player.transform.position.x <= -12 || player.transform.position.x >= 12) && player.transform.position.y < -3f)//lado esquerdo
        {
            Vector3 aux = new Vector3(1.0f,0.0f,0.0f);
            rb2d.velocity = -aux;
            if (player.transform.position.x < 0) margem_lateral = -margem_lateral;
            player.transform.position = new Vector2(player.transform.position.x - margem_lateral, 8.97f);
        }
        //Wall2
        if ((player.transform.position.x <= -12 || player.transform.position.x >= 12) && player.transform.position.y < 10f)//lado esquerdo
        {
            Vector3 aux = rb2d.velocity;
            rb2d.velocity = -aux;
            if (player.transform.position.x < 0) margem_lateral = -margem_lateral;
            player.transform.position = new Vector2(player.transform.position.x - margem_lateral, -3.18f);
        }

    }
	public void activateStore(){
		gameObject.GetComponentInChildren<Canvas> ().enabled = !gameObject.GetComponentInChildren<Canvas> ().enabled;
	}
    // Update is called once per frame
    void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            //if(grounded)
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal, jumpPower);

        if (Input.GetKeyDown (KeyCode.B)) {
			activateStore ();
		}

    
    }
}
