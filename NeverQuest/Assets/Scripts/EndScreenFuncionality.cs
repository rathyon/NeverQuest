using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenFuncionality : MonoBehaviour {

    public GameObject Restart, Leaderboard, ThirdPerson, endScreen, saveButton;



    public InputField mainInputField;

    // Use this for initialization
    void Start () {
        Restart.GetComponent<Button>().onClick.AddListener(RestartScript);


        mainInputField.ActivateInputField();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void closetop10Script() {
        endScreen.SetActive(false);
    }

    public void endTopScreen()
    {
        ThirdPerson.GetComponent<PlayerController>().top10.SetActive(false);
    }

    void RestartScript()
    {
        SceneManager.LoadScene("First Room");
    }
}
