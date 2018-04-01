using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOnce : MonoBehaviour {

	private float _damage;

	// Use this for initialization
	void Start () {
		_damage = 50;
	}
	
	public float damage(){
		return _damage;
	}
}
