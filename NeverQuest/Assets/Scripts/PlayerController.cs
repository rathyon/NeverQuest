using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    //public bool grounded = true;
    public float jumpPower;
    private float waitedTime, inactiveTimerMAX;

    public int gold;
    public bool facingRight = true;

    public GameObject player;
    public List<MobController> enemies = new List<MobController>();

    //public List<DoorController> Player_doorsCatched = new List<DoorController>();
    public DoorController[] allDoors;

    public bool storeActive;
    private Button[] bton;

    // shooting variables
    public GameObject bullet;
    private bool canShoot = true;
    public float bullet_damage;
    public float shootCooldown;
    private float shootTimeRemaining;

    //flamethrower variables
    public GameObject flamethrower;
    public bool canFlamethrower = true;
    public bool flamethrowerOn = false;
    public float flamethrowerCooldown;
    private float flamethrowerTimeRemaining;

    public bool grounded;

    public Text questWarning;
    public float timeAccept;
    public bool questIsBeingAccepted = false;
    public float timeToAcceptQuest = 5.0f;
    private bool anyAccepting = false;



    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    public int transportLevel;


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bton = gameObject.GetComponentsInChildren<Button>();
        foreach (Button b in bton)
        {
            b.interactable = false;
        }
        //questWarning.text = "";
        timeAccept = 5.0f;
        bullet_damage = 10.0f;
        storeActive = false;
        grounded = true;
        transportLevel = 3;
        gold = 200;
        waitedTime = 0.0f;
        inactiveTimerMAX = 1.45f;

        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

		Vector3 x = GameObject.Find ("Trap_Description").transform.localScale;
		x.x = 0;
		GameObject.Find ("Trap_Description").transform.localScale = x;
		GameObject.Find ("WA_Description").transform.localScale = x;

    }

    private void LateUpdate()
    {

        //check if any enemy is currently accepting quest

        //		foreach (MobController enemy in enemies)
        //		{
        //			if (enemy != null)
        //			{
        //				var enemy_script = enemy;
        //
        //				if (enemy_script.isAcceptingQuest())
        //				{
        //					questIsBeingAccepted = true;
        //					anyAccepting = true;
        //					if (enemy_script.questAcceptTime <= timeToAcceptQuest)
        //					{
        //						timeToAcceptQuest = enemy_script.questAcceptTime;
        //					}
        //				}
        //			}
        //		}
        if (!anyAccepting)
        {
            questIsBeingAccepted = false;
            timeToAcceptQuest = timeAccept;
        }

        if (questIsBeingAccepted)
        {
            questWarning.text = "QUEST ACCEPTED IN: " + ((int)timeToAcceptQuest + 1);
            timeToAcceptQuest -= Time.deltaTime;
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
        if (!storeActive)
        {
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

    private void FireFlamethrower()
    {
        if (canFlamethrower && !flamethrowerOn)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            Instantiate(flamethrower, position, Quaternion.identity);
            canFlamethrower = false;
            flamethrowerOn = true;
            flamethrowerTimeRemaining = flamethrowerCooldown;
        }
    }

    private void ShootProjectile()
    {
        if (canShoot)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            Instantiate(bullet, position, Quaternion.identity);
            canShoot = false;
            shootTimeRemaining = shootCooldown;
        }
    }

    public void activateStore()
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
        storeActive = !storeActive;
        gameObject.GetComponentInChildren<Canvas>().enabled = !gameObject.GetComponentInChildren<Canvas>().enabled;
        foreach (Button b in bton)
        {
            b.interactable = !b.interactable;
        }
    }


    // Update is called once per frame
    void Update()
    {
        TimerJump();
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            if (!storeActive)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal, jumpPower);
                grounded = false;
            }

        }

        if (!canShoot)
        {
            if (shootTimeRemaining <= 0)
            {
                canShoot = true;
            }
            else
            {
                shootTimeRemaining -= Time.deltaTime;
            }
        }

        if (!canFlamethrower)
        {
            if (flamethrowerTimeRemaining <= 0)
            {
                canFlamethrower = true;
            }
            else
            {
                flamethrowerTimeRemaining -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            activateStore();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            FireFlamethrower();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            flamethrowerOn = false;
        }
    }

    public void AddGold(int _gold)
    {
        gold += _gold;
    }

    public void TimerJump()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            questIsBeingAccepted = true;
            anyAccepting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            questIsBeingAccepted = false;
            anyAccepting = false;
        }
    }
}
