using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    //public bool grounded = true;
    public float jumpPower;
    private float waitedTime, inactiveTimerMAX;

    public int gold;
    public bool facingRight = true;

    public GameObject player;
    public List<MobController> enemies;

    public List<DoorController> Player_doorsCatched = new List<DoorController>();

    private bool storeActive;
	private Button[] bton;

	public GameObject bullet;
	private bool shoot;
	public float bullet_damage; 

	public bool qHabilityFlag;
	public GameObject qHability;

    public bool grounded;

	public Text goldText;
	public Text questWarning;
	private bool questIsBeingAccepted = false;
	public float timeToAcceptQuest = 5.0f;

   

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    public int transportLevel;


	// Use this for initialization
	void Start () {
		bton = gameObject.GetComponentsInChildren<Button> ();
		foreach (Button b in bton) {
			b.interactable = false;
		}
		//questWarning.text = "";
		qHabilityFlag = false;
		shoot = false;
		bullet_damage = 10.0f;
		storeActive = false;
        grounded = true;
        transportLevel = 3;
        gold = 200;
        waitedTime = 0.0f;
        inactiveTimerMAX = 1.45f;

        gameObject.GetComponentInChildren<Canvas> ().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
		StartCoroutine(ShootProjectile());
		StartCoroutine(QHability());
	}

	private void LateUpdate()
	{
		goldText.text = "Gold: " + gold.ToString();

		//check if any enemy is currently accepting quest
		bool anyAccepting = false;

		foreach (MobController enemy in enemies)
		{
			if (enemy != null)
			{
				var enemy_script = enemy;

				if (enemy_script.isAcceptingQuest())
				{
					questIsBeingAccepted = true;
					anyAccepting = true;
					if (enemy_script.questAcceptTime <= timeToAcceptQuest)
					{
						timeToAcceptQuest = enemy_script.questAcceptTime;
					}
				}
			}
		}
		if (!anyAccepting)
		{
			questIsBeingAccepted = false;
			timeToAcceptQuest = 5.0f;
		}

		if (questIsBeingAccepted)
		{
			questWarning.text = "QUEST ACCEPTED IN: " + ((int)timeToAcceptQuest + 1);
		}
		else
		{
			questWarning.text = "";
		}

		if (timeToAcceptQuest <= 0.0f)
		{
			questWarning.text = "GAME OVER";
			Time.timeScale = 0;
		}
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
	private IEnumerator ShootProjectile()
	{
		while (true) {
			yield return new WaitForSeconds (1);
			if (shoot) {
				Vector3 position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
				Instantiate (bullet, position, Quaternion.identity);
			}
		}
	}

	private IEnumerator QHability()
	{
		while (true) {
			yield return new WaitForSeconds (7);
			if (qHabilityFlag) {
				Vector3 position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
				Instantiate (qHability, position, Quaternion.identity);
			}
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
        TimerJump();
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
			if (!storeActive) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveHorizontal, jumpPower);
				grounded = false;
			}

        }

        if (Input.GetKeyDown (KeyCode.B)) {
			activateStore ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			shoot = true;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			shoot = false;
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			qHabilityFlag = true;
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			qHabilityFlag = false;
		}
    }

    public void AddGold(int _gold) {
        gold += _gold;
    }

    public void TimerJump() {
        if (!grounded)
        {
            waitedTime += Time.deltaTime;
            if (waitedTime >= inactiveTimerMAX)
            {
                waitedTime = 0.0f;
                grounded = true;
            }
        }
    }
}
