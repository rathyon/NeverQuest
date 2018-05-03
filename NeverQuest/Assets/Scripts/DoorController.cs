using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject linkedDoor;
    public int DoorLevel, DoorToNextLevel;


	// Use this for initialization
	void Start () {
        DoorToNextLevel = linkedDoor.GetComponent<DoorController>().DoorLevel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.R) && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().transform.position = linkedDoor.transform.position;
            collision.GetComponent<PlayerController>().Player_doorsCatched.Add(this);

            foreach (MobController x in collision.GetComponent<PlayerController>().enemies.ToArray())
                if (!x.doorsToCatch.Contains(this.linkedDoor.GetComponent<DoorController>())) x.doorsToCatch.Add(this);
                else x.doorsToCatch.Remove(this.linkedDoor.GetComponent<DoorController>());

            collision.GetComponent<PlayerController>().transportLevel = DoorToNextLevel;
        }
        if (collision.gameObject.CompareTag("Mob") && collision.GetComponent<MobController>().canTransport && collision.GetComponent<MobController>().doorsToCatch.Count != 0 && this == collision.GetComponent<MobController>().doorsToCatch.ToArray()[0])
        {
            collision.GetComponent<MobController>().transform.position = linkedDoor.transform.position;
            collision.GetComponent<MobController>().mobTransportLevel = DoorToNextLevel;
            collision.GetComponent<MobController>().doorsToCatch.Remove(this);
            collision.GetComponent<MobController>().canTransport = false;
            //DelayToDoorFlag(3, collision.GetComponent<MobController>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mob")) other.GetComponent<MobController>().canTransport = true;
    }
}
