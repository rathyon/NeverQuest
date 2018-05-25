using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
public class EquipmentStoreControler : Selectable {
    public int type;

    BaseEventData m_BaseEvent;


    private PlayerController playerControler;

    public GameObject button;
    public GameObject button2;

    public int tier;

    private WeaponsANDArmor butScript, but2Script;

	// Use this for initialization
	void Start () {

        playerControler = GetComponentInParent<PlayerController>();
        butScript = button.GetComponent<WeaponsANDArmor>();
        but2Script = button2.GetComponent<WeaponsANDArmor>();
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (tier == 1)
        {
            Vector3 x = button.transform.localScale;
            x.x = 1;
            button.transform.localScale = x;
            x.x = 0;
            button2.transform.localScale = x;
        }
        else if (tier == 2)
        {
            Vector3 x = button.transform.localScale;
            x.x = 0;
            button.transform.localScale = x;
            x.x = 1;
            button2.transform.localScale = x;
        }

        //Check if the GameObject is being highlighted
        if (IsHighlighted(m_BaseEvent) == true)
        {
            
            Vector3 x = GameObject.Find("Trap_Description").transform.localScale;
            x.x = 0;
            GameObject.Find("Trap_Description").transform.localScale = x;
            x.x = 1;
            GameObject.Find("WA_Description").transform.localScale = x;
            if (type == 0)
            {
                if (tier == 1)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Leather Armor";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "Increases the time of quest acceptance, but no one really knows why, it just does";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Time increased to 6";
                }

                if (tier == 2)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Steel Armor";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "Increases the time of quest acceptance, but no one really knows why, it just does";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Time increased to 7";
                }
            }
            if (type == 1)
            {
                if (tier == 1)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Rusty Shotgun";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "Old shotgun the owner probably already died, carefull not to cut yourself, you might catch gangrene";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Damage increased to 15";

                }

                if (tier == 2)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Golden Shotgun";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "The mother of all shotguns, a product definitely made by a dwarf, or a gnome...well anyone with the expertise";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Damage increased to 25";
                }
            }
            if (type == 2){
                if (tier == 1)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Wheelchair";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "A wheelchair from Primark";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Speed increased to 7";

                }

                if (tier == 2)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "iWheelchair";
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "The chicks dig it, what are you waiting for?Buy it.";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Speed increases 0";
                }
            }

        }
        else{
            Color coisa = gameObject.GetComponent<RawImage>().color;
            coisa.r = 86;
            coisa.g = 79;
            coisa.b = 70;
            gameObject.GetComponent<RawImage>().color = coisa;
        }
    }
    public void tierIncrease(){
        tier++;
    }
    void TaskOnClick()
    {

        if (type == 0)
        {
            if (tier == 1)
            {
                if (playerControler.gold >= 50)
                {
                    playerControler.timeAccept = 6f;
                    playerControler.gold -= 50;
                }
            }
            if (tier == 2)
            {
                if (playerControler.gold >= 200)
                {
                    playerControler.timeAccept = 7f;
                    playerControler.gold -= 200;
                    gameObject.GetComponent<RawImage>().color=new Color(255, 0, 0);
                }

            }
        }
        if (type == 1)
        {
            if (tier == 1)
            {
                if (playerControler.gold >= 100)
                {
                    playerControler.bullet_damage = 15.0f;
                    playerControler.gold -= 100;
                }
            }
            if (tier == 2)
            {
                if (playerControler.gold >= 250)
                {
                    playerControler.bullet_damage = 25.0f;
                    playerControler.gold -= 250;

                    gameObject.GetComponent<RawImage>().color = new Color(255, 0, 0);
                }
            }
        }

        if (type == 2)
        {
            if (tier == 1)
            {
                if (playerControler.gold >= 50)
                {
                    playerControler.speed = 3f;
                    playerControler.gold -= 50;
                }
            }
            if (tier == 2)
            {
                if (playerControler.gold >= 200)
                {
                    playerControler.speed = 3f;
                    playerControler.gold -= 200;

                    gameObject.GetComponent<RawImage>().color = new Color(255, 0, 0);
                }
            }
        }

        tier++;
    }
}
