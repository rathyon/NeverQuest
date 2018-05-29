using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    /* Remover depois*/
    public int points = 0;
    int hp = 100;
    /* ============== */
    /* Stats variables*/
    public int numTimePlayed = 0, numDoorsCatched = 0, numMobsKilled = 0, numFlamethrowerUsed = 0, numBulletsUsed = 0, numTrapsUsed = 0, numBearTrap = 0, numFireTrap = 0, numDDOSTrap = 0, numMoneyTrap = 0, numIronMaidenTrap = 0, numPoisonTrap = 0;
    public GameObject FloatingText, GoldAnim;
    public float playedTime;
    /* ============== */

    /* Screens variables*/
    private string _nickname;

    public Dictionary<string, int> Leaderboard;

    public List<GameObject> nicks_Data;
    public List<GameObject> scores_Data;

    public bool paused, endGame, canWrite = true;
    public GameObject pauseScreen, endScreen, saveNickname, saveButton, leadButton, top10;
    public Text InputNickname, endPointsText;

    public Text TryAgainText, IsOnTOP10, IsOnTOP10_2;

    /*==================*/
    public float speed;

    //public bool grounded = true;
    public float jumpPower;
    private float waitedTime, inactiveTimerMAX;

    public int gold;
    public bool facingRight = true;

    public GameObject player;
    public List<MobController> enemies = new List<MobController>();
    public List<float> WavesTimers = new List<float>();

    public SimpleHealthBar cooldown_bar;

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
    public bool hasGun = false;

    //flamethrower variables
    public GameObject flamethrower;
    public bool canFlamethrower = true;
    public bool flamethrowerOn = false;
    public float flamethrowerCooldown; 
    public float flamethrowerTimeRemaining;
    public bool hasFlameThrower = false;

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
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        endGame = true;
        playedTime = 0.0f;

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
        //transportLevel = 3;
        gold = 250;
        waitedTime = 0.0f;
        inactiveTimerMAX = 1.45f;

        paused = true;
        flamethrowerTimeRemaining = flamethrowerCooldown;

        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        Leaderboard = new Dictionary<string, int>();

        leadButton.GetComponent<Button>().onClick.AddListener(leadButtonScript);
        saveButton.GetComponent<Button>().onClick.AddListener(saveButtonScript);

        Vector3 x = GameObject.Find ("Trap_Description").transform.localScale;
		x.x = 0;
		GameObject.Find ("Trap_Description").transform.localScale = x;
		GameObject.Find ("WA_Description").transform.localScale = x;

        // WAIT FOR START BUTTON CLICK
        Time.timeScale = 0;
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

        if (timeToAcceptQuest <= 0.0f) //EndGame
        {
            questWarning.text = "GAME OVER";
            //Time.timeScale = 0;
            endGame = true;
            numTimePlayed = Mathf.RoundToInt(playedTime);

            if (canWrite)
            {
                this.GetComponentInParent<ReadWriteTxt>().WavesWriteFile();
                this.GetComponentInParent<ReadWriteTxt>().WritePlayerStats();
                print("PLAYERCONTROLLER");
                this.GetComponentInParent<ReadWriteTxt>().ActualizeOverviewStats();
                canWrite = false;
            }

                IsOnTOP10.text = "You are too weak for NEVERQUEST TOP10 ...";
                IsOnTOP10_2.text = "";


            endScreen.SetActive(true);
            saveNickname.SetActive(false);
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
		if (!flamethrowerOn && !storeActive && hasFlameThrower)
        {
            Vector3 position = new Vector3(transform.position.x-0.2f, transform.position.y , transform.position.z);
            Instantiate(flamethrower, position, Quaternion.identity);
            canFlamethrower = false;
            flamethrower.GetComponent<EngQHability>().maxLifeTime = (flamethrowerTimeRemaining);
            numFlamethrowerUsed++;
        }

        flamethrowerOn = !flamethrowerOn;
    }

    private void ShootProjectile()
    {
		if (canShoot && !storeActive && hasGun)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            Instantiate(bullet, position, Quaternion.identity);
            numBulletsUsed++;
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

 

    public void showFloatingText(GameObject _gameObject, Vector3 position, int XP)
    {
        GameObject txtAnim;
        txtAnim = Instantiate(_gameObject, (position + new Vector3(+1f, 5.5f, 0f)), Quaternion.identity);
        txtAnim.GetComponentInChildren<Text>().text = " + " + XP + "XP";


    }

    public void showGoldAnimText(GameObject _gameObject, Vector3 position, int GOLD)
    {
            GameObject txtAnim;
    txtAnim = Instantiate(_gameObject, position, Quaternion.identity);
        txtAnim.GetComponentInChildren<Text>().text = " + " + GOLD;
        print(txtAnim.GetComponentInChildren<Text>().text);
    }

    // Update is called once per frame
    void Update()
    {
        TimerJump();

        if (!endGame)
        {
            playedTime += Time.deltaTime;
            //print(Mathf.RoundToInt(playedTime) + "ASL \n");
        }


        if (flamethrowerTimeRemaining <= 0.0f)
        {
            flamethrowerTimeRemaining = 0.0f;
            flamethrowerOn = false;

        }

        endPointsText.text = "" + points + "";


        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            if (!storeActive)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, jumpPower);
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

        if (!flamethrowerOn && (flamethrowerTimeRemaining < flamethrowerCooldown))
        {
            flamethrowerTimeRemaining += (Time.deltaTime * 0.3f);
        }
        if (flamethrowerOn && (flamethrowerTimeRemaining > 0.0f)) { flamethrowerTimeRemaining -= (Time.deltaTime * 1.2f); }


        if (!canFlamethrower)
        {
            if (flamethrowerTimeRemaining <= 0)
            {
                canFlamethrower = true;
                flamethrowerTimeRemaining = 0;
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

        /*Verifica a pausa*/
        if (Input.GetKeyDown(KeyCode.P) && !endGame)
        {
            paused = !paused;
            if (paused)
            {
                pauseScreen.SetActive(true);
                PauseGame();
            }
            else
            {
                pauseScreen.SetActive(false);
                ContinueGame();
            }



        }
        /**/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(hasGun) ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            FireFlamethrower();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            flamethrowerOn = false;
        }

        cooldown_bar.UpdateBar(flamethrowerTimeRemaining, flamethrowerCooldown);
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

    /*Screens functions*/
    public void closeEndScreenScript()
    {
        endScreen.SetActive(false);
    }

    public void leadButtonScript()
    {
        atualizaLeaderBoard();
        top10.SetActive(true);
    }

    public void saveButtonScript()
    {
        InputNicknameScript();
        top10.SetActive(true);
    }
    private void InputNicknameScript()
    {
//        print("ENTREIIIIIIII : " + this.GetComponentInParent<ReadWriteTxt>().nicknames.Count);
  //      print("ENTRADO: " + InputNickname.text);
        //print("Count: " + ThirdPerson.GetComponent<GameControler>().GetComponentInParent<ReadWriteTxt>().nicknames.Count);
        if (this.GetComponentInParent<ReadWriteTxt>().nicknames.Count == 10)
        {
            this.GetComponentInParent<ReadWriteTxt>().nicknames.RemoveAt(-1);
            this.GetComponentInParent<ReadWriteTxt>().scores.RemoveAt(-1);
        }
        int roundScore = Mathf.RoundToInt((1 / playedTime) * 1000000);
        AddToArraysNickScore(InputNickname.text, roundScore);
        atualizaLeaderBoard();
        this.GetComponentInParent<ReadWriteTxt>().WriteFile();
    }
    public void SetScore(string username, int score, Dictionary<string, int> _leaderboard)
    {
        if (username != "")
        {
            if (Leaderboard.ContainsKey(username) == false)
            {
                Leaderboard.Add(username, score);
            }
            else
                Leaderboard[username] = score;
        }
    }

    public void atualizaLeaderBoard()
    {

        for (int x = 0; x < this.GetComponentInParent<ReadWriteTxt>().nicknames.Count; x++)
        {
            // print("line : " + this.GetComponentInParent<ReadWriteTxt>().nicknames[x] + " score : " + this.GetComponentInParent<ReadWriteTxt>().scores[x]);
           SetScore(this.GetComponentInParent<ReadWriteTxt>().nicknames[x], this.GetComponentInParent<ReadWriteTxt>().scores[x], this.Leaderboard);
            print("Nickname: " + this.GetComponentInParent<ReadWriteTxt>().nicknames[x] + "\n Score: " + this.GetComponentInParent<ReadWriteTxt>().scores[x] + "\n");
        }

        this.GetComponentInParent<ReadWriteTxt>().nicknames = new List<string>();
        this.GetComponentInParent<ReadWriteTxt>().scores = new List<int>();
        //// Sorted by Key

        //print("Sorted by Key");
        //print("=============");
        //foreach (KeyValuePair<string, int> author in Leaderboard.OrderBy(key => key.Key))
        //{
        //    print("Key: " + author.Key + ", Value: " + author.Value + "");
        //}
        //print("=============");


        foreach (KeyValuePair<string, int> author in Leaderboard.OrderBy(key => key.Value))
        {
            this.GetComponentInParent<ReadWriteTxt>().nicknames.Add(author.Key);
            this.GetComponentInParent<ReadWriteTxt>().scores.Add(author.Value);
            //print("Key: "+ author.Key + ", Value: " + author.Value + "");
        }

        this.GetComponentInParent<ReadWriteTxt>().nicknames.Reverse();
        this.GetComponentInParent<ReadWriteTxt>().scores.Reverse();

        Leaderboard = new Dictionary<string, int>();

        for (int x = 0; x < this.GetComponentInParent<ReadWriteTxt>().nicknames.Count; x++)
        {
            //print("line : " + this.GetComponentInParent<ReadWriteTxt>().nicknames[x] + " score : " + this.GetComponentInParent<ReadWriteTxt>().scores[x]);
            SetScore(this.GetComponentInParent<ReadWriteTxt>().nicknames[x], this.GetComponentInParent<ReadWriteTxt>().scores[x], this.Leaderboard);
        }
        print("OLHAI AI  " + this.GetComponentInParent<ReadWriteTxt>().nicknames.Count);

        for (int i = 0; i < this.GetComponentInParent<ReadWriteTxt>().nicknames.Count; i++)
        {
            // print("nicknamescount" + this.gameObject.GetComponent<ReadWriteTxt>().nicknames.Count);
            nicks_Data[i].GetComponent<Text>().text = this.GetComponentInParent<ReadWriteTxt>().nicknames[i];


        }

        for (int i = 0; i < this.GetComponentInParent<ReadWriteTxt>().scores.Count; i++)
        {
            // print("scorescount" + this.gameObject.GetComponent<ReadWriteTxt>().scores.Count);
            scores_Data[i].GetComponent<Text>().text = "" + this.GetComponentInParent<ReadWriteTxt>().scores[i] + "";

        }

    }

    public int GetScore(string username, int score)
    {
        if (Leaderboard.ContainsKey(username) == false)
        {
            return 0;
        }
        if (Leaderboard["" + username + ""] != score)
        {
            return 0;
        }
        return Leaderboard[username];
    }


    //public int ChangeScore(string username, int score)
    //{

    //}



    public void OrdenaScores()
    {
        this.GetComponentInParent<ReadWriteTxt>().nicknames.Sort();
    }

    public bool CanBeOnLeaderboard(int _score)
    {

        //if (this.GetComponentInParent<ReadWriteTxt>().scores.Count == 10) return false;
        //else
        //{fl

        //  }

        foreach (int x in this.GetComponentInParent<ReadWriteTxt>().scores.ToArray())
        {
            if (_score > x) return true;
        }
        return false;
    }


    public void AddToArraysNickScore(string nick, int score)
    {
        this.GetComponentInParent<ReadWriteTxt>().nicknames.Add(nick);
        this.GetComponentInParent<ReadWriteTxt>().scores.Add(score);
    }

    public void pauseScreenON()
    {
        pauseScreen.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void pauseScreenOFF()
    {
        pauseScreen.SetActive(false);
        //enable the scripts again
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        //enable the scripts again
    }

    public void addPoints(int x)
    {
        points += x;
    }

    public void damage(int x)
    {
        hp -= x;
    }
}
