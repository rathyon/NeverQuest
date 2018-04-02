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

    private Rigidbody2D rb2d;
    private Vector3 playerPos;
    private bool timerActive;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        statsInfo.text = "Enemy hp: " + HP.ToString();
        playerPos = player.transform.position;
        timerActive = false;
	}
	
	// Update is called once per frame
	void Update () {

        playerPos = player.transform.position;

        if (timerActive)
        {
            Countdown();
            if (questAcceptTime <= 0.0f)
            {
                Destroy(player);
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
        if (collision.gameObject.CompareTag("TrapOnce"))
        {
            var trap = collision.GetComponent<TestTrapController>();

            HP -= trap.damage;
            statsInfo.text = "Enemy hp: " + HP.ToString();
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
