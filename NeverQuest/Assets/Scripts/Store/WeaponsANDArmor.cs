using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class WeaponsANDArmor : Selectable {
	private int type;//0-armor 1-weapon
	public bool active;

	public Button yourButton;

	private PlayerController playerControler;

	private bool canUpdate;

	BaseEventData m_BaseEvent;

	void Start()
	{
		canUpdate = true;
		playerControler = GetComponentInParent<PlayerController> ();
		active = true;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	}

    void TaskOnClick()
    {
        if (active){
            int tier = yourButton.GetComponent<EquipmentStoreControler>().tier;
            //yourButton.GetComponent<EquipmentStoreControler>().tierIncrease();
            type = yourButton.GetComponent<EquipmentStoreControler>().type;

            if (type == 0)
            {
                if (tier == 1)
                {
                    if (playerControler.gold >= 150)
                    {
                        playerControler.timeAccept = 5.5f;
                        playerControler.jumpPower = 3;
                        playerControler.gold -= 150;

                        active = false;
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
                        playerControler.bullet_damage = 7.0f;
                        playerControler.gold -= 100;

                        active = false;
                    }
                }
                if (tier == 2)
                {
                    if (playerControler.gold >= 150)
                    {
                        playerControler.hasFlameThrower = true;
                        playerControler.bullet_damage = 12.0f;
                        playerControler.gold -= 150;
                        active = false;
                    }
                }
                if (tier == 3)
                {
                    if (playerControler.gold >= 200)
                    {
                        playerControler.bullet_damage = 24.0f;
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

                        active = false;
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

    }
	}
	
