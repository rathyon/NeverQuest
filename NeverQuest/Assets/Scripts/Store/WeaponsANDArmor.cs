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
        int tier = yourButton.GetComponent<EquipmentStoreControler>().tier;
        yourButton.GetComponent<EquipmentStoreControler>().tierIncrease();
        type = yourButton.GetComponent<EquipmentStoreControler>().type;

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
                        active = false;
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
                        active = false;
                    }
                }
            }

	}

		
	}
	
