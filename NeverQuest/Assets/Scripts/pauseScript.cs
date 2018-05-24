using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseScript : MonoBehaviour{

    public GameObject Resume, Leaderboard, closetop10, pauseScreen;
    public GameObject ThirdPerson;

    // Use this for initialization
    void Start()
    {
        Resume.GetComponent<Button>().onClick.AddListener(ResumeScript);
        Leaderboard.GetComponent<Button>().onClick.AddListener(LeaderboardScript);
        closetop10.GetComponent<Button>().onClick.AddListener(QuitScript);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void endTopScreen()
    {
        ThirdPerson.GetComponent<PlayerController>().top10.SetActive(false);
    }

    void ResumeScript()
    {
        //Output this to console when the Button is clicked
        Debug.Log("You have clicked the button!1");

        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        ThirdPerson.GetComponent<PlayerController>().paused = false;
    }
    void LeaderboardScript()
    {
        ThirdPerson.GetComponent<PlayerController>().atualizaLeaderBoard();
        ThirdPerson.GetComponent<PlayerController>().top10.SetActive(true);
    }
    void QuitScript()
    {
        Time.timeScale = 1;
        ThirdPerson.GetComponent<PlayerController>().paused = false;
        pauseScreen.SetActive(false);
    }

}
