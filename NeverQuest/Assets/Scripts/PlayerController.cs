using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int gold;
    public Text goldText;
	public bool buymodeActive;

	public bool facingright = true;
    //private Rigidbody2D rb2d;

	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		buymodeActive = false;
	}

    private void LateUpdate()
    {
        goldText.text = "Gold: " + gold.ToString();
    }

	void Update(){
		if (Input.GetKeyDown (KeyCode.B)) {
			buymodeActive = !buymodeActive;
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
}
