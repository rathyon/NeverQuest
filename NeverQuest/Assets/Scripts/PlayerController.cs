using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    //public bool grounded = true;
    public float jumpPower;

    public int gold;
    public bool facingRight = true;

    public GameObject player;
    public List<GameObject> enemies;

    public List<DoorController> Player_doorsCatched = new List<DoorController>();

    private bool storeActive;
	private Button[] bton;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    public int transportLevel;


	// Use this for initialization
	void Start () {
		bton = gameObject.GetComponentsInChildren<Button> ();
		foreach (Button b in bton) {
			b.interactable = false;
		}
		storeActive = false;
        transportLevel = 3;
        gold = 200;
		gameObject.GetComponentInChildren<Canvas> ().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
		if (!storeActive){
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

	        transform.position += movement * speed * 0.1f;
	        //rb2d.AddForce(movement * speed);
		}

    }
	public void activateStore(){
		storeActive = !storeActive;
		gameObject.GetComponentInChildren<Canvas> ().enabled = !gameObject.GetComponentInChildren<Canvas> ().enabled;
		foreach (Button b in bton) {
			b.interactable = !b.interactable;
		}
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

    public void AddGold(int _gold) {
        gold += _gold;
    }
}
