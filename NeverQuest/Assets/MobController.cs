
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float Tail1_maxPoint; // direira: 12.0f;
    public float Tail1_minPoint; // esquerda: -12.0f


    public int HP;
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
			}
			else if(player.transform.position.x < transform.position.x)
			{
				transform.position -= new Vector3(speed * slowPercentage * 0.025f, 0.0f);
			}
			else
			{
				//do nothing...
			}
		}

        //Wall1
        if ((transform.position.x <= -12 || transform.position.x >= 12) && transform.position.y == -3.18f)//lado esquerdo
        {
            Vector3 aux = rb2d_mob.velocity;
            rb2d_mob.velocity = -aux;
            if (transform.position.x < 0) margem_lateral = -margem_lateral;
            transform.position = new Vector2(transform.position.x - margem_lateral, 8.97f);
           
        }
        //Wall2
        if ((transform.position.x <= -12 || transform.position.x >= 12) && transform.position.y == 8.97f)//lado esquerdo
        {
            Vector3 aux = rb2d_mob.velocity;
            rb2d_mob.velocity = -aux;
            if (transform.position.x < 0) margem_lateral = -margem_lateral;
            transform.position = new Vector2(transform.position.x - margem_lateral, -3.18f);
            
        }
    }
    private void Countdown()
    {
        questAcceptTime -= Time.deltaTime;
    }


}