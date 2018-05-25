using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class StoreTabSelector : Selectable {
	public int type;

	BaseEventData m_BaseEvent;

	private bool canUpdate;

    private Button[] bton;
    private StoreItemClickController[] trapScripts;
    private WeaponsANDArmor[] weapArmScripts;

	public void Start()
	{
		canUpdate = true;
	}
	void Update()
	{
		if (IsHighlighted (m_BaseEvent) == true) {
            if (type == 1)
            {
                Vector3 x = GameObject.Find("Traps").transform.localScale;
                x.x = 1;
                GameObject.Find("Traps").transform.localScale = x;
                x.x = 0;
                GameObject.Find("Weapons/Armor").transform.localScale = x;
                bton = GameObject.Find("Weapons/Armor").GetComponentsInChildren<Button>();
                foreach (Button b in bton)
                {
                    b.interactable = false;
                }
                weapArmScripts = GameObject.Find("Weapons/Armor").GetComponentsInChildren<WeaponsANDArmor>();
                foreach (WeaponsANDArmor b in weapArmScripts)
                {
                    b.interactable = false;
                }
                bton = GameObject.Find("Traps").GetComponentsInChildren<Button>();
                foreach (Button b in bton)
                {
                    b.interactable = true;
                }
                trapScripts = GameObject.Find("Traps").GetComponentsInChildren<StoreItemClickController>();
                foreach (StoreItemClickController b in trapScripts)
                {
                    b.interactable = true;
                }
            }
				else if (type == 2) {
                    Vector3 x = GameObject.Find("Weapons/Armor").transform.localScale;
                    x.x = 1;
                    GameObject.Find("Weapons/Armor").transform.localScale = x;
                    x.x = 0;
                    GameObject.Find("Traps").transform.localScale = x;
                    bton = GameObject.Find("Traps").GetComponentsInChildren<Button>();
                    foreach (Button b in bton)
                    {
                        b.interactable = false;
                    }
                    trapScripts = GameObject.Find("Traps").GetComponentsInChildren<StoreItemClickController>();
                    foreach (StoreItemClickController b in trapScripts)
                    {
                        b.interactable = false;
                    }
                    bton = GameObject.Find("Weapons/Armor").GetComponentsInChildren<Button>();
                    foreach (Button b in bton)
                    {
                        b.interactable = true;
                    }
                    weapArmScripts = GameObject.Find("Weapons/Armor").GetComponentsInChildren<WeaponsANDArmor>();
                    foreach (WeaponsANDArmor b in weapArmScripts)
                    {
                        b.interactable = true;
                    }

				} 
		
			}

	}

}
