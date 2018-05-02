
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public float HP;
    private float HP_max;
    public Text labelHP;
    public GameObject player;
    public GameObject proximityIndicator;
    public GameObject minimapIndicator;

    public DoorController[] doorsAvaiables;
    public List<DoorController> doorsToCatch = new List<DoorController>();

    int playerTransportLevel;
    public int mobTransportLevel;

    public bool flag_change = false;
    public bool canTransport = true;
    //private bool flag_TryHard = false;

    public int flag_Teste;
    public float speed, questAcceptTime;
    private float posToFollow;

    //private Rigidbody2D rb2d;
    private bool timerActive;
    public bool slowed;
    public float slowPercentage;
    private float slowTimer;
    public float slowTimerMAX;
	public bool facingRight;


    public SimpleHealthBar healthBar;
    SpriteRenderer spriteRenderer_mob;
    Rigidbody2D rb2d_mob;

    // Use this for initialization
    void Start()
    {
        flag_Teste = 0;
        //speed = 1.5f;
		//HP = 100;
        HP_max = HP;
        slowPercentage = 1.0f;
		timerActive = false;
        spriteRenderer_mob = GetComponent<SpriteRenderer>();
        rb2d_mob = GetComponent<Rigidbody2D>();

        minimapIndicator = Instantiate(minimapIndicator, transform.position, Quaternion.identity);
    }

    int estado_antigo;
    // Update is called once per frame
    void Update()
    {

		if (HP <= 0)
		{
			proximityIndicator.GetComponent<ProximityIndicatorController>().removeEnemy(gameObject);
			Destroy(minimapIndicator);
			Destroy(gameObject);
			player.GetComponent<PlayerController>().AddGold(50); //Definir um valor fixo para quando se mata um mob
		}
        //HP -= 0.1f; //Barra HP_mob
        healthBar.UpdateBar(HP, HP_max);
        minimapIndicator.transform.position = transform.position;

        //movimento inteligente do mob
        playerTransportLevel = player.GetComponent<PlayerController>().transportLevel;
        doorsToCatch = player.GetComponent<PlayerController>().Player_doorsCatched;

        if (player.GetComponent<PlayerController>().transportLevel != mobTransportLevel) //player e mob nao estao na mesma sala
        {
            if (player.GetComponent<PlayerController>().Player_doorsCatched.Count == 0)
            {
                findDoor(mobTransportLevel, player.GetComponent<PlayerController>().transportLevel);
                posToFollow = doorsToCatch[0].transform.position.x;
            }

            else posToFollow = player.GetComponent<PlayerController>().Player_doorsCatched[0].transform.position.x;

            if (posToFollow > transform.position.x) 
            {
                transform.position += new Vector3(speed * slowPercentage * 0.025f, 0.0f);
                facingRight = true;
            }
            else if (posToFollow < transform.position.x)
            {
                transform.position -= new Vector3(speed * slowPercentage * 0.025f, 0.0f);
                facingRight = false;
            }
        }
        else
        { // simple tracking movement just in the x axis
            if (player.transform.position.x > transform.position.x)
            {
                transform.position += new Vector3(speed * slowPercentage * 0.025f, 0.0f);
                facingRight = true;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                transform.position -= new Vector3(speed * slowPercentage * 0.025f, 0.0f);
                facingRight = false;
            }
            //Se tiverem no mesmo nível, limpa o array das portas de ambos
            if (player.GetComponent<PlayerController>().Player_doorsCatched.Count != 0) foreach (DoorController door_aux in player.GetComponent<PlayerController>().Player_doorsCatched) player.GetComponent<PlayerController>().Player_doorsCatched.Remove(door_aux);
        }
			
		if (slowed)
		{
			slowTimer += Time.deltaTime;
			if (slowTimer >= slowTimerMAX)
			{
				slowPercentage=1.0f;
				slowTimer = 0;
				slowed = false;
			}
		}
		if (timerActive)
		{
			Countdown();
		}
    }

    private void Countdown()
    {
        questAcceptTime -= Time.deltaTime;
    }

    private void findDoor(int mobLevel, int playerLevel)
    {
        List<DoorController> doors_aux = new List<DoorController>(), doors_lvl = new List<DoorController>();
        foreach (DoorController door in doorsAvaiables)
        {
            if (door.DoorLevel == mobLevel) //door com acesso direto
            {
                if (door.DoorToNextLevel == playerLevel)
                {
                    doors_aux.Add(door); break;
                }
                else doors_lvl.Add(door); 
            }

        }
        if (doors_aux.Count == 0) { doors_aux.Add(doors_lvl[0]); findDoor(doors_lvl[0].DoorToNextLevel, playerLevel); }

        foreach (DoorController door2 in doors_aux) doorsToCatch.Insert(0, door2);
    }
}