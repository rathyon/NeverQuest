using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Trap : MonoBehaviour {
	public int placementX;//so that the position is fine with all of them
	public int placementY;// some traps would b correctly placed whereas other types wont

	public int cost;
	public int _damage;
	public bool active;
	public float slowPercentage; //0-1
	public float _stopTime;
	public float _slowTime;

	public string trapName; //for store

}
