using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestMobController : MonoBehaviour {

	public int HP;
	public Text statsInfo;
	public GameObject player;
	public float speed;
	public float questAcceptTime;

	//private Rigidbody2D rb2d;
	private Vector3 playerPos;
	private bool timerActive;
	private bool slowed;
	private float slowPercentage;
	private float slowTimer;
	private float slowTimerMAX;

	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D>();
		statsInfo.text = "Enemy hp: " + HP.ToString();
		playerPos = player.transform.position;
		timerActive = false;
		slowed = false;
		slowTimerMAX = 2.0f;
	}

	// Update is called once per frame
	void Update () {

		playerPos = player.transform.position;
		if (slowed) {
			slowTimer += Time.deltaTime;
			if (slowTimer >= slowTimerMAX){
				speed = 3.0f;
				slowTimer = 0;
				slowed = false;
			}
		}
		if (timerActive)
		{
			Countdown();
			if (questAcceptTime <= 0.0f)
			{
				//Destroy(player);
			}
		}
		else
		{
			// simple tracking movement just in the x axis
			if (playerPos.x - 0.5f > transform.position.x)
			{
				transform.position += new Vector3(speed * 0.025f, 0.0f);
			}
			else if (playerPos.x + 0.5f < transform.position.x)
			{
				transform.position -= new Vector3(speed * 0.025f, 0.0f);
			}
			else
			{
				//do nothing...
			}
		}

		if(HP <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void Countdown()
	{
		questAcceptTime -= Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BearTrap"))
		{
			var trap = collision.GetComponent<TestTrapController>();
			slowTimer = 0.0f;
			slowTimerMAX = 2.0f;
			slowed = true;
			slowPercentage = 0.3f;
			HP -= trap.damage;
			statsInfo.text = "Enemy hp: " + HP.ToString();
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

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			timerActive = true;
		}
	}
}
