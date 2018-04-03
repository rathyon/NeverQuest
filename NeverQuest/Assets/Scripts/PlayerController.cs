using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int gold;
    public Text goldText;
	public bool buymodeActive;
    public float attackCD;
    private bool attacking = false;

    public GameObject sword;

	public bool facingright = true;
    //private Rigidbody2D rb2d;

	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();

		_spriteRenderer = GetComponent<SpriteRenderer> ();
		buymodeActive = false;
        sword.SetActive(false);
		//sword.SetActive(true);
	}

    private void LateUpdate()
    {
        goldText.text = "Gold: " + gold.ToString();
    }

	void Update(){
		if (Input.GetKeyDown (KeyCode.B))
        {
			buymodeActive = !buymodeActive;
			//sword.SetActive(false);
		}
        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
	}

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		if (_spriteRenderer != null) {
			if (moveHorizontal < 0 && facingright) {
				facingright = !facingright;
				//_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
			if (moveHorizontal > 0 && !facingright) {
				facingright = !facingright;
				//_spriteRenderer.flipX = !_spriteRenderer.flipX;
			}
		}

        transform.position += movement * speed * 0.1f;
	}

    private void Attack()
    {
		if (!attacking && !buymodeActive)
        {
            StartCoroutine("swing");
        }
    }

    IEnumerator swing()
    {
        attacking = true;
        Vector3 swingPos;
        if (facingright)
        {
            swingPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
            sword.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            swingPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
            sword.GetComponent<SpriteRenderer>().flipX = true;
        }

        
        sword.transform.position = swingPos;
        sword.SetActive(true);
        yield return new WaitForSeconds(attackCD);
        sword.SetActive(false);
        attacking = false;
    }
}
