using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject linkedDoor;
    public int level, nextLevel;


    // Use this for initialization
    void Start()
    {
        nextLevel = linkedDoor.GetComponent<DoorController>().level;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.R) && collision.gameObject.CompareTag("Player"))
        {
            //move player to othet dorr
            collision.GetComponent<PlayerController>().transform.position = linkedDoor.transform.position;

            //collision.GetComponent<PlayerController>().Player_doorsCatched.Add(this);

            foreach (MobController mob in collision.GetComponent<PlayerController>().enemies.ToArray())
                if (!mob.PathToPlayer.Contains(this.linkedDoor.GetComponent<DoorController>()))
                {
                    mob.PathToPlayer.Add(this);
                    //Debug.Log("Player changed floor, I added!");
                }
                else
                {
                    mob.PathToPlayer.Remove(this.linkedDoor.GetComponent<DoorController>());
                    //Debug.Log("Player changed floor, I removed!");
                }

            collision.GetComponent<PlayerController>().transportLevel = nextLevel;
        }


        if (collision.gameObject.CompareTag("Mob") && collision.GetComponent<MobController>().canTransport && collision.GetComponent<MobController>().PathToPlayer.Count != 0 && this == collision.GetComponent<MobController>().PathToPlayer.ToArray()[0])
        {
            collision.GetComponent<MobController>().transform.position = linkedDoor.transform.position;
            collision.GetComponent<MobController>().currentFloor = nextLevel;
            collision.GetComponent<MobController>().PathToPlayer.Remove(this);
            collision.GetComponent<MobController>().canTransport = false;
            //DelayToDoorFlag(3, collision.GetComponent<MobController>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mob")) other.GetComponent<MobController>().canTransport = true;
    }
}
