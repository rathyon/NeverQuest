using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDeath : MonoBehaviour {


    public float DestroyTime = 3f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, DestroyTime);
	}
}
