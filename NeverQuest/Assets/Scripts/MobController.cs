
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float HP;
    private float MaxHP;
    public int goldReward, XPReward;

    public Text HPBarText;

    public GameObject player;
    public GameObject proximityIndicator;
    public GameObject minimapIndicator;
    public GameObject wavesManager;
    public GameObject FloatingText;
    public GameObject GoldAnim;

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

        findDoor(currentFloor, playerFloor, currentFloor);
    }

    private void findDoor(int mobLevel, int playerLevel, int lastLevel)
    {
        if (mobLevel == playerLevel) return; //Porque a funcao agora é chamada no start, logo temos de garantir que mobLevel != playerLevel

        List<DoorController> path = new List<DoorController>();
        List<DoorController> doorsInCurrentLevel = new List<DoorController>();
        bool final_door = false;

        // for all doors in the level
        foreach (DoorController door in player.GetComponent<PlayerController>().allDoors)
        {
            if (door.level == mobLevel)
            {
                if (door.nextLevel == playerLevel)
                {
                    path.Add(door);
                    final_door = true;
                    break;
                }
                else
                {
                    doorsInCurrentLevel.Add(door);
                }
            }

        }
        if (!final_door && doorsInCurrentLevel.Count != 0)
        {
            //this is for the spawn point, where there only is one door
            // se o floor tiver mais que 1 porta, tenho de escolher a melhor (pode ter 2 ou 3)
            // doorsInCurrentLevel; door.nextlevel

            DoorController doorToCatch = findBestDoorAux(doorsInCurrentLevel, playerLevel, lastLevel);

            path.Add(doorToCatch);
            findDoor(doorToCatch.nextLevel, playerLevel, doorToCatch.level);
        }

        foreach (DoorController door2 in path.ToArray())
            PathToPlayer.Insert(0, door2);
    }

    DoorController findBestDoorAux(List<DoorController> lista_door, int playerLvl, int old_lvl) {
        bool flag_aux = true;
        DoorController best_door = new DoorController();
        int max, max_aux=100;

        //print(lista_door.Count);
        foreach (DoorController door in lista_door) {
            if (door.nextLevel != old_lvl)
            {
                if (flag_aux)
                {
                    max_aux = Mathf.Abs(playerLvl - door.nextLevel); // door.nextlevel mais proximo do playerlvl;
                    flag_aux = false;
                }
                max = Mathf.Abs(playerLvl - door.nextLevel);

                if (Mathf.Abs(playerLvl - door.nextLevel) <= max_aux)
                {
                    max_aux = max;
                    best_door = door;
                   // Debug.Log("in");
                }
            }
        }
        //Debug.Break();

        return best_door;
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
            //player.GetComponent<PlayerController>().showFloatingText(FloatingText, transform.position, XPReward);
            player.GetComponent<PlayerController>().showGoldAnimText(GoldAnim, GoldAnim.GetComponent<RectTransform>().position, goldReward);


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