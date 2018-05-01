
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

    public DoorController[] doorsAvaiables;
    public List<DoorController> doorsToCatch = new List<DoorController>();


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
    public int playerTransportLevel, mobTransportLevel;

    public SimpleHealthBar healthBar;
    SpriteRenderer spriteRenderer_mob;
    Rigidbody2D rb2d_mob;

    // Use this for initialization
    void Start()
    {
        flag_Teste = 0;
        speed = 1.5f;
		HP = 100;
        HP_max = HP;
        slowPercentage = 1.0f;
		timerActive = false;
        spriteRenderer_mob = GetComponent<SpriteRenderer>();
        rb2d_mob = GetComponent<Rigidbody2D>();
    }

    int estado_antigo;
    // Update is called once per frame
    void Update()
    {
        //HP -= 0.1f; //Barra HP_mob
        //healthBar.UpdateBar(HP, HP_max);

        //movimento inteligente do mob
        doorsToCatch = player.GetComponent<PlayerController>().Player_doorsCatched;

        if (player.GetComponent<PlayerController>().transportLevel != mobTransportLevel) //player e mob nao estao na mesma sala
        {
            //if (player.GetComponent<PlayerController>().Player_doorsCatched.Count != 0)

            //foreach (DoorController _door in findDoor(mobTransportLevel, player.GetComponent<PlayerController>().transportLevel)) doorsToCatch.Add(_door);
          
            posToFollow = player.GetComponent<PlayerController>().Player_doorsCatched[0].transform.position.x;

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
            //if (doorsToCatch.Count != 0) foreach (DoorController door_aux in doorsToCatch) doorsToCatch.Remove(door_aux);
            if (player.GetComponent<PlayerController>().Player_doorsCatched.Count != 0) foreach (DoorController door_aux in player.GetComponent<PlayerController>().Player_doorsCatched) player.GetComponent<PlayerController>().Player_doorsCatched.Remove(door_aux);
        }

  //      if (HP <= 0)
		//{
		//	Destroy(gameObject);
  //          player.GetComponent<PlayerController>().AddGold(50); //Definir um valor fixo para quando se mata um mob
  //      }
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
      //  playerTransportLevel = player.GetComponent<PlayerController>().transportLevel;
    }
    //public void movementIA() { //Encontro a door que devo apanhar.
    //    int nextLevel; bool existsdoor = false;

    //    foreach (DoorController door in doorsAvaiables)
    //    {
    //        if (player.GetComponent<PlayerController>().transportLevel == door.DoorToNextLevel && mobTransportLevel == door.DoorLevel)
    //        {
    //            doorsToCatch.Add(door); //Visto que não existem portas que levem para o mesmo nível.
    //            existsdoor = true;
    //           // posToFollow = door.transform.position.x;
    //        }
    //    }
    //    if (!existsdoor)
    //    {
    //        //Caso em que n
    //        if (player.GetComponent<PlayerController>().transportLevel > mobTransportLevel) nextLevel = mobTransportLevel + 1; //temos de subir de nivel
    //        else nextLevel = mobTransportLevel - 1;//temos de descer de nível

    //        foreach (DoorController door in doorsAvaiables)
    //        {
    //            if (nextLevel == door.DoorToNextLevel && mobTransportLevel == door.DoorLevel)
    //            {
    //                doorsToCatch.Add(door);
    //            }
    //        }
    //    }
    //}

    private void Countdown()
    {
        questAcceptTime -= Time.deltaTime;
    }

    //private List<DoorController> findDoor(int mobLevel, int playerLevel) {
    //    List<DoorController> doors_aux = new List<DoorController>();
    //    int count = 0;
    //    foreach (DoorController door in doorsAvaiables) {
    //        if (door.DoorLevel == mobLevel) //door com acesso direto
    //        {
    //            if (count == 1) {
    //                if (doors_aux[0].DoorToNextLevel == playerLevel) break;
    //                else
    //                {
    //                    doors_aux.Remove(doors_aux[0]);
    //                    doors_aux.Add(door);
    //                }
    //            }
    //            else
    //            {
    //                doors_aux.Add(door);
    //                count = 1;
    //            }
    //        }
    //    }
    //    return doors_aux;
    //}

}