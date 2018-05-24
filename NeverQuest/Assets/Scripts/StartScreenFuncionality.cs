using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenFuncionality : MonoBehaviour {

    public GameObject StartButton, LeaderboardButton, ThirdPerson;

    // Use this for initialization
    void Start () {
        StartButton.GetComponent<Button>().onClick.AddListener(StartScript);
        LeaderboardButton.GetComponent<Button>().onClick.AddListener(LeaderboardScript);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartScript()
    {
        //Output this to console when the Button is clicked
        print("StartScript");

        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        ThirdPerson.GetComponent<PlayerController>().paused = false;
        ThirdPerson.GetComponent<PlayerController>().endGame = false;

    }
    void LeaderboardScript()
    {
        ThirdPerson.GetComponent<PlayerController>().atualizaLeaderBoard();
        ThirdPerson.GetComponent<PlayerController>().top10.SetActive(true);
    }
}
