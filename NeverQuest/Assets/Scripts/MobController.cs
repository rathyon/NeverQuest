
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float HP;
    private float MaxHP;
    public int goldReward;

    public Text HPBarText;

    public GameObject player;
    public GameObject proximityIndicator;
    public GameObject minimapIndicator;
    public GameObject wavesManager;

    public List<DoorController> PathToPlayer = new List<DoorController>();

    public int currentFloor;
    private int playerFloor;
    public bool canTransport = true;

    public float speed, questAcceptTime;

    public float targetPos;

    private bool timerActive = false;
    public bool slowed;
    public float slowPercentage;
    private float slowTimer;
    public float slowTimerMAX;
    public bool facingRight;

    public SimpleHealthBar healthBar;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    private bool acceptingQuest = false;

    public bool isAcceptingQuest()
    {
        return acceptingQuest;
    }

    // Use this for initialization
    void Start()
    {
        MaxHP = HP;
        slowPercentage = 1.0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        player.GetComponent<PlayerController>().enemies.Add(this);
        minimapIndicator = Instantiate(minimapIndicator, transform.position, Quaternion.identity);
        playerFloor = player.GetComponent<PlayerController>().transportLevel;

        findDoor(currentFloor, playerFloor);
    }

    private void findDoor(int mobLevel, int playerLevel)
    {
        List<DoorController> path = new List<DoorController>();
        List<DoorController> doorsInCurrentLevel = new List<DoorController>();

        // for all doors in the level
        foreach (DoorController door in player.GetComponent<PlayerController>().allDoors)
        {
            if (door.level == mobLevel)
            {
                if (door.nextLevel == playerLevel)
                {
                    path.Add(door);
                    break;
                }
                else
                {
                    doorsInCurrentLevel.Add(door);
                }
            }

        }
        if (path.Count == 0)
        {
            //this is for the spawn point, where there only is one door
            path.Add(doorsInCurrentLevel[0]);
            findDoor(doorsInCurrentLevel.ToArray()[0].nextLevel, playerLevel);
        }

        foreach (DoorController door2 in path.ToArray())
            PathToPlayer.Insert(0, door2);

        path.Clear();
        doorsInCurrentLevel.Clear();
    }

    private void moveTo(float target)
    {
        if (target > transform.position.x)
        {
            transform.position += new Vector3(speed * slowPercentage * 0.025f, 0.0f);
            facingRight = true;
        }
        else if (target < transform.position.x)
        {
            transform.position -= new Vector3(speed * slowPercentage * 0.025f, 0.0f);
            facingRight = false;
        }
    }

    private void updateSpeed()
    {
        if (slowed)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer >= slowTimerMAX)
            {
                slowPercentage = 1.0f;
                slowTimer = 0;
                slowed = false;
            }
        }
    }

    private void Countdown()
    {
        questAcceptTime -= Time.deltaTime;
    }

    void Update()
    {

        if (HP <= 0)
        {
            wavesManager.GetComponent<WavesManagerController>().EnemySlain();
            proximityIndicator.GetComponent<ProximityIndicatorController>().removeEnemy(gameObject);
            Destroy(minimapIndicator);
            player.GetComponent<PlayerController>().AddGold(goldReward); //Definir um valor fixo para quando se mata um mob
            player.GetComponent<PlayerController>().enemies.Remove(this);

            Destroy(gameObject);
        }

        // update HUD stuff
        healthBar.UpdateBar(HP, MaxHP);
        minimapIndicator.transform.position = transform.position;

        updateSpeed();

        //get player's current floor
        playerFloor = player.GetComponent<PlayerController>().transportLevel;

        //if mob is not on player's floor
        if (playerFloor != currentFloor)
        {
            moveTo(PathToPlayer[0].transform.position.x);
        }
        // if on same floor
        else
        {
            moveTo(player.transform.position.x);
        }

        //Se tiverem no mesmo nível, limpa o array das portas de ambos
        /*
        foreach (DoorController door_aux in player.GetComponent<PlayerController>().Player_doorsCatched.ToArray())
            player.GetComponent<PlayerController>().Player_doorsCatched.Remove(door_aux);
        foreach (DoorController door_aux2 in doorsToCatch.ToArray())
            doorsToCatch.Remove(door_aux2);
            */

        if (timerActive)
        {
            Countdown();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timerActive = true;
            acceptingQuest = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timerActive = false;
            acceptingQuest = false;
        }
    }
}