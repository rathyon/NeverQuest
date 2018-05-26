﻿using System.Collections;
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
    public GameObject button3;
    public GameObject myself;

    public bool active = true;

    public int tier;


	// Use this for initialization
	void Start () {

        playerControler = GetComponentInParent<PlayerController>();
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
            button3.transform.localScale = x;
        }
        else if (tier == 2)
        {
            Vector3 x = button.transform.localScale;
            x.x = 0;
            button.transform.localScale = x;
            x.x = 1;
            button2.transform.localScale = x;
        }
        else if (tier == 3)
        {
            if (type == 1)
            {

                Vector3 x = button2.transform.localScale;
                x.x = 0;
                button2.transform.localScale = x;
                x.x = 1;
                button3.transform.localScale = x;
            }
           /* else
            {
                Color x = myself.GetComponent<Button>().colors.normalColor;
                x = Color.red;
                myself.GetComponent<Button>().colors.normalColor.g = 0f;
            }*/
           
        }
        else myself.GetComponent<Image>().color = Color.red;
    
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
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[0].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "Increases the time of quest acceptance, but no one really knows why and its light materials mean you can finally jump! At 26.. :)";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Time increased to 5.5s  +0.5";
                }

                if (tier == 2)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Steel Armor";
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[1].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "Increases the time of quest acceptance blablabla, you know...same as previous but better.";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Time increased to 6  +0.5";
                }
            }
            if (type == 1)
            {
                if (tier == 1)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Rusty Shotgun";
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[0].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "You'll unluck the hability to shoot with this Old shotgun. The owner probably already died and carefull not to cut yourself, you might catch gangrene";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Damage increased to 3 +3";

                }

                if (tier == 2)
                {

                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Regular Shotgun";
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[1].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "!!Free flamethrower included!! Regarding the shotgun itself? Nothing special honestly.";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Damage increased to 6 +3";
                }
                if (tier == 3)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Golden Shotgun";

                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[2].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = " The mother of all shotguns, a product definitely made by a dwarf, or a gnome...well anyone with the expertise.";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Damage increased to 13 +7";}
            }
            if (type == 2){
                if (tier == 1)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "Wheelchair";
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[0].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "A wheelchair from Samsung! You can finally run fas... go faster!";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Speed increased to 1.9  +0.9";

                }

                if (tier == 2)
                {
                    GameObject.Find("Description_Name_WA").GetComponent<Text>().text = "iWheelchair";
                    GameObject.Find("Description_Image_WA").GetComponent<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[1].sprite;
                    GameObject.Find("Description_Text_WA").GetComponent<Text>().text = "The chicks dig it, what are you waiting for?Buy it.";
                    GameObject.Find("Description_Damage_Text_WA").GetComponent<Text>().text = "Speed increases 2.2  +0.3";
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
        if (active)
        {
            if (type == 0)
            {
                if (tier == 1)
                {
                    if (playerControler.gold >= 150)
                    {
                        playerControler.timeAccept = 5.5f;
                        playerControler.jumpPower = 3;
                        playerControler.gold -= 150;

                    }
                }
                if (tier == 2)
                {
                    if (playerControler.gold >= 200)
                    {
                        playerControler.timeAccept = 6f;
                        playerControler.jumpPower = 7;
                        playerControler.gold -= 200;
                        active = false;
                    }
                }
            }
            if (type == 1)
            {
                if (tier == 1)
                {
                    if (playerControler.gold >= 100)
                    {
                        playerControler.hasGun = true;
                        playerControler.bullet_damage = 3.0f;
                        playerControler.gold -= 100;

                    }
                }
                if (tier == 2)
                {
                    if (playerControler.gold >= 150)
                    {
                        playerControler.hasFlameThrower = true;
                        playerControler.bullet_damage = 6.0f;
                        playerControler.gold -= 150;
                    }
                }
                if (tier == 3)
                {
                    if (playerControler.gold >= 200)
                    {
                        playerControler.bullet_damage = 10.0f;
                        playerControler.gold -= 200;
                        active = false;
                    }
                }
            }

            if (type == 2)
            {
                if (tier == 1)
                {
                    if (playerControler.gold >= 200)
                    {
                        playerControler.speed = 1.9f;
                        playerControler.gold -= 150;

                    }
                }
                if (tier == 2)
                {
                    if (playerControler.gold >= 799)
                    {
                        playerControler.speed = 2.2f;
                        playerControler.gold -= 799;
                        active = false;
                    }
                }
            }

        }
        tier++;
    }
}
