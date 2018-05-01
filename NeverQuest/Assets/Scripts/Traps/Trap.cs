using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Trap : MonoBehaviour {
	public float placementX;//so that the position is fine with all of them
	public float placementY;// some traps would b correctly placed whereas other types wont

	public int cost;
	public float _damage;
	public bool active;
	public float slowPercentage; //0-1
	public float _stopTime;
	public float _slowTime;

	public string trapName; //for store

}
