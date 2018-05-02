using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour {
	public Text textGold;
	// Use this for initialization
	void Start () {
		Text textGold = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		textGold.text = GetComponentInParent<PlayerController> ().gold.ToString ();
	}
}
