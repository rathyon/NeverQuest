using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobControler : MonoBehaviour {

	public float speedOriginal;
	public int HP;
	//public Text labelHP;
	public GameObject player;
	public float speed;
	public bool slowed;
	public float slowTimerMAX;
	public float speedSlowPercentage;

	//private Rigidbody2D rb2d;
	private Vector3 playerPos;
	private bool timerActive;
	private float slowPercentage;
	private float slowTimer;

	private bool acceptingQuest = false;

	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D>();
		//statsInfo.text = "Enemy hp: " + HP.ToString();
		speedSlowPercentage = 1.0f;
		playerPos = player.transform.position;
		timerActive = false;
		slowed = false;
		slowTimerMAX = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (HP <= 0)
		{
			Destroy(gameObject);
		}
		playerPos = player.transform.position;
		if (Time.timeScale != 0)
		{
			if (slowed)
			{
				slowTimer += Time.deltaTime;
				if (slowTimer >= slowTimerMAX)
				{
					speed /= speedSlowPercentage;
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
				if (playerPos.x > transform.position.x)
				{
					transform.position += new Vector3(speed * 0.025f, 0.0f);
				}
				else if(playerPos.x < transform.position.x)
				{
					transform.position -= new Vector3(speed * 0.025f, 0.0f);
				}
				else
				{
					//do nothing...
				}
			}
			//labelHP.text = "HP: " + HP;
		}
	}

	private void Countdown()
	{
		//questAcceptTime -= Time.deltaTime;
	}

	public bool isAcceptingQuest()
	{
		return acceptingQuest;
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
{
	if (collision.gameObject.CompareTag("BearTrap"))
	{
		var trap = collision.GetComponent<TestTrapController>();
		slowTimer = 0.0f;
		slowTimerMAX = 2.0f;
		slowed = true;
		slowPercentage = 0.3f;
		HP -= trap.damage;
		//statsInfo.text = "Enemy hp: " + HP.ToString();
		Destroy(GameObject.Find (collision.gameObject.name));
		speed = 1.0f;

	}
	if (collision.gameObject.CompareTag("MoneyTrap"))
	{
		var trap = collision.GetComponent<TestTrapController>();
		slowTimer = 0.0f;
		slowTimerMAX = 4.0f;
		slowed = true;
		speed = 0.0f;
		Destroy(GameObject.Find (collision.gameObject.name));
	}
	if (collision.gameObject.CompareTag("IronMaidenTrap"))
	{
		var trap = collision.GetComponent<TestTrapController>();
		if (!trap.wait) {
			Destroy (gameObject);
			trap.wait = true;
		}
	}
    if (collision.gameObject.CompareTag("PlayerAttack"))
    {
        HP -= 20;
        //statsInfo.text = "Enemy hp: " + HP.ToString();
    }

}*/

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			timerActive = true;
			acceptingQuest = true;
		}
	}
}


