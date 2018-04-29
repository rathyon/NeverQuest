﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnerController : MonoBehaviour{

    public GameObject testEnemy;
    public GameObject player;
    // stores X coordinates of spawnPoints
    // ATTENTION: CURRENTLY SPAWNING AT Y = 0 <-- NEEDS TO BE A PARAMETER IN THE FUTURE
    public int[] spawnPoints;
    public float minSpawnDelay, maxSpawnDelay;
    public int prepDuration;
    public int actionDuration;
    public int goldReward;
    public int numOfWaves;
    public Text timerText;

    private int timeLeft;
    private bool phase = false; //false = prep phase, true = action phase
    private int wavesBeat = 0;
    private bool beatGame = false;


    private void Start()
    {
        timeLeft = prepDuration;
        timerText.text = "Time before next Wave: " + timeLeft;
        StartCoroutine(PlayTimer());
    }

    private void Update()
    {
        if (!phase)
        {
            if (!beatGame)
                timerText.text = "Time before next Wave: " + timeLeft;
            else
                timerText.text = "You win!";
        }
        else
        {
            timerText.text = "" + timeLeft;
        }
    }

    // increments the play time for use in other stuff
    IEnumerator PlayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;

            if(timeLeft <= 0)
            {
                // if action phase ends...
                if (phase)
                {
                    if (wavesBeat >= numOfWaves)
                    {
                        phase = !phase;
                        StopCoroutine("SpawnEnemies");
                        Time.timeScale = 0;
                        beatGame = true;
                    }
                    else
                    {
                        phase = !phase;
                        timeLeft = prepDuration;
                        StopCoroutine("SpawnEnemies");
                        player.GetComponent<PlayerController>().GiveGold(goldReward);
                        wavesBeat++;
                    }
                }
                // if prep phase ends...
                else
                {
                    phase = !phase;
                    timeLeft = actionDuration;
                    StartCoroutine("SpawnEnemies");
                }
            }
        }
    }


    IEnumerator SpawnEnemies()
    {
        while (phase)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            Vector3 spawnPos = new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)], 0, 0);

            GameObject newEnemy = Instantiate(testEnemy, spawnPos, Quaternion.identity);

            player.GetComponent<PlayerController>().AddEnemy(newEnemy);
        }
    }
}
