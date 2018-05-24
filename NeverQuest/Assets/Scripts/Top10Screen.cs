using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Top10Screen : MonoBehaviour {

    public GameObject closeButton, ThirdPerson;
    // Use this for initialization
    void Start () {
        closeButton.GetComponent<Button>().onClick.AddListener(closeButtonScript);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void closeButtonScript()
    {
        ThirdPerson.GetComponent<PlayerController>().top10.SetActive(false);
    }
}
