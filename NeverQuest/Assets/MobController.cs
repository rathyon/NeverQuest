
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float Tail1_maxPoint; // direira: 12.0f;
    public float Tail1_minPoint; // esquerda: -12.0f


    public float HP;
    public Text labelHP;
    public GameObject player;



    public bool flag_change = false;


    public float speed;
    public float questAcceptTime;

    //private Rigidbody2D rb2d;
    private bool timerActive;
    public bool slowed;
    public float slowPercentage;
    private float slowTimer;
    public float slowTimerMAX;
	public bool facingRight; 

    SpriteRenderer spriteRenderer_mob;
    Rigidbody2D rb2d_mob;

    // Use this for initialization
    void Start()
    {
		speed = 1.5f;
		HP = 100;
		slowPercentage = 1.0f;
		timerActive = false;
        spriteRenderer_mob = GetComponent<SpriteRenderer>();
        rb2d_mob = GetComponent<Rigidbody2D>();
    }

    int estado_antigo;
    // Update is called once per frame
    void Update()
    {
		int margem_lateral = 2;
		if (HP <= 0)
		{
			Destroy(gameObject);
		}
		if (slowed)
		{
			slowTimer += Time.deltaTime;
			if (slowTimer >= slowTimerMAX)
			{
				slowPercentage=1.0f;
				slowTimer = 0;
				slowed = false;
			}
		}
		if (timerActive)
		{
			Countdown();
		}
		else
		{
			// simple tracking movement just in the x axis
			if (player.transform.position.x > transform.position.x)
			{
				transform.position += new Vector3(speed * slowPercentage * 0.025f, 0.0f);
				facingRight = true;
			}
			else if(player.transform.position.x < transform.position.x)
			{
				transform.position -= new Vector3(speed * slowPercentage * 0.025f, 0.0f);
				facingRight = false;
			}
			else
			{
				//do nothing...
			}
		}

    }
    private void Countdown()
    {
        questAcceptTime -= Time.deltaTime;
    }


}