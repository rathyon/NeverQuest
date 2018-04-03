using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int gold;
    public Text goldText;
    public Text questWarning;
    public bool buymodeActive;
    public float attackCD;
    public GameObject sword;
    public bool facingright = true;

    private bool attacking = false;
    private List<GameObject> enemies;
    private bool questIsBeingAccepted = false;
    private float timeBeforeQuestAccepted = 1000000.0f;
    //private Rigidbody2D rb2d;

	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();

		_spriteRenderer = GetComponent<SpriteRenderer> ();
		buymodeActive = false;
        sword.GetComponent<BoxCollider2D>().enabled = false;
        sword.SetActive(false);
        //sword.SetActive(true);
        questWarning.text = "";
        enemies = new List<GameObject>();
    }

    private void LateUpdate()
    {
        goldText.text = "Gold: " + gold.ToString();

        //check if any enemy is currently accepting quest
        bool anyAccepting = false;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                var enemy_script = enemy.GetComponent<TestMobController>();

                if (enemy_script.isAcceptingQuest())
                {
                    questIsBeingAccepted = true;
                    anyAccepting = true;
                    if (enemy_script.questAcceptTime <= timeBeforeQuestAccepted)
                    {
                        timeBeforeQuestAccepted = enemy_script.questAcceptTime;
                    }
                }
            }
        }
        if (!anyAccepting)
        {
            questIsBeingAccepted = false;
            timeBeforeQuestAccepted = 1000000.0f;
        }

        if (questIsBeingAccepted)
        {
            questWarning.text = "QUEST ACCEPTED IN: " + ((int)timeBeforeQuestAccepted + 1);
        }
        else
        {
            questWarning.text = "";
        }

        if (timeBeforeQuestAccepted <= 0.0f)
        {
            questWarning.text = "GAME OVER";
            Time.timeScale = 0;
        }
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

        if(Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale != 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
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

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
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
        sword.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(attackCD);
        sword.GetComponent<BoxCollider2D>().enabled = false;
        sword.SetActive(false);
        attacking = false;
    }
}
