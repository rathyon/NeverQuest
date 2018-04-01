using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestMobController : MonoBehaviour {

    public int HP;
    public Text statsInfo;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        statsInfo.text = "Enemy hp: " + HP.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(-0.025f, 0.0f);
		if(HP <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            var trap = collision.GetComponent<TestTrapController>();

            HP -= trap.damage;
            statsInfo.text = "Enemy hp: " + HP.ToString();
        }
    }

}
