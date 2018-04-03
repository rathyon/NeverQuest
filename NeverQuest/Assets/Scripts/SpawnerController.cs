using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnerController : MonoBehaviour{

    public GameObject testEnemy;
    // stores X coordinates of spawnPoints
    // ATTENTION: CURRENTLY SPAWNING AT Y = 0 <-- NEEDS TO BE A PARAMETER IN THE FUTURE
    public int[] spawnPoints;
    public float minSpawnDelay, maxSpawnDelay;
    public int prepDuration;
    public int actionDuration;
    public Text timerText;

    private int timeLeft;
    private bool phase = false; //false = prep phase, true = action phase


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
            timerText.text = "Time before next Wave: " + timeLeft;
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
                    phase = !phase;
                    timeLeft = prepDuration;
                    StopCoroutine("SpawnEnemies");
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

            Instantiate(testEnemy, spawnPos, Quaternion.identity);
        }
    }
}

