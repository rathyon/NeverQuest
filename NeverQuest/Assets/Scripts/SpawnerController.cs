using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{

    public GameObject player, MobOriginal, RusherMob, BruiserMob;

    // stores X coordinates of spawnPoints

    // ATTENTION: CURRENTLY SPAWNING AT Y = 0 <-- NEEDS TO BE A PARAMETER IN THE FUTURE

    public List<Vector4> spawnPoints = new List<Vector4>();

    public int waveString = 0;

    public int prepDuration;
    public int actionDuration;
    public int goldReward;
    public int numOfWaves;
    public Text timerText;

    private int cenas = 3;

    private int timeLeft;
    private bool phase = false; //false = prep phase, true = action phase
    private int wavesBeat = 0;
    private bool beatGame = false;


    private void Start()
    {
        spawnPoints.Add(new Vector4(-24.5f, 11.1f, 1f, 5)); // mobTransport = 5;
        spawnPoints.Add(new Vector4(24f, 14f, 1f, 6)); // mobTransport = 6;
        spawnPoints.Add(new Vector4(-24.5f, 11.1f, 1f, 1)); // mobTransport = 1;

        timeLeft = prepDuration;
        timerText.text = "Time before next Wave: " + timeLeft;
        StartCoroutine(PlayTimer());
    }

    private void Update()
    {
        if (!phase)
        {
            //if (!beatGame)
            //   // timerText.text = "Time before next Wave: " + timeLeft;
            //else
            //   // timerText.text = "You win!";
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

            if (timeLeft <= 0)
            {
                // if action phase ends...
                if (phase)
                {
                    if (wavesBeat >= numOfWaves)
                    {
                        phase = !phase;
                        StopCoroutine("SpawnEnemies");
                        //Time.timeScale = 0;
                        beatGame = true;
                    }
                    else
                    {
                        phase = !phase;
                        timeLeft = prepDuration;
                        StopCoroutine("SpawnEnemies");
                        player.GetComponent<PlayerController>().AddGold(goldReward);
                        wavesBeat++;
                    }
                }
                // if prep phase ends...
                else
                {
                    phase = !phase;
                    timeLeft = actionDuration;
                    cenas = 3;
                    StartCoroutine("SpawnEnemies");
                }
            }
        }
    }


    IEnumerator SpawnEnemies()
    {
        waveString++;
        while (cenas >= 0)
        {
            yield return new WaitForSeconds(1);
            // 1 wave: 5 RusherEnemy + 3 MobEnemy    /  2 wave: 7 RusherEnemy + 4  MobEnemy + 1 BruiserEnemy /  2 wave: 7 RusherEnemy + 5  MobEnemy + 2 BruiserEnemy 
            if (waveString == 1)
            {
                int x = Random.Range(0, 2);
                GameObject MobEnemy = Instantiate(MobOriginal, spawnPoints[x], Quaternion.identity);
                MobEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                MobEnemy.GetComponent<MobController>().player = player;

                x = Random.Range(0, 2);
                GameObject RusherEnemy = Instantiate(RusherMob, spawnPoints[x], Quaternion.identity);
                RusherEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                RusherEnemy.GetComponent<MobController>().player = player;

                x = Random.Range(0, 2);
                GameObject RusherEnemy1 = Instantiate(RusherMob, spawnPoints[x], Quaternion.identity);
                RusherEnemy1.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                RusherEnemy1.GetComponent<MobController>().player = player;



                player.GetComponent<PlayerController>().enemies.Add(MobEnemy.GetComponent<MobController>());

                player.GetComponent<PlayerController>().enemies.Add(RusherEnemy.GetComponent<MobController>());
                player.GetComponent<PlayerController>().enemies.Add(RusherEnemy1.GetComponent<MobController>());

            }
            else if (waveString == 2)
            {
                int x = Random.Range(0, 2);
                GameObject _MobEnemy = Instantiate(MobOriginal, spawnPoints[x], Quaternion.identity);
                _MobEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                _MobEnemy.GetComponent<MobController>().player = player;
                x = Random.Range(0, 2);
                GameObject _MobEnemy1 = Instantiate(MobOriginal, spawnPoints[x], Quaternion.identity);
                _MobEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                _MobEnemy.GetComponent<MobController>().player = player;
                x = Random.Range(0, 2);


                x = Random.Range(0, 2);
                GameObject _RusherEnemy = Instantiate(RusherMob, spawnPoints[x], Quaternion.identity);
                _RusherEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                _RusherEnemy.GetComponent<MobController>().player = player;

                x = Random.Range(0, 2);
                GameObject _RusherEnemy1 = Instantiate(RusherMob, spawnPoints[x], Quaternion.identity);
                _RusherEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                _RusherEnemy.GetComponent<MobController>().player = player;
                x = Random.Range(0, 2);



                x = Random.Range(0, 2);
                GameObject _BruiserEnemy = Instantiate(BruiserMob, spawnPoints[x], Quaternion.identity);
                _BruiserEnemy.GetComponent<MobController>().currentFloor = (int)spawnPoints[x].w;
                _BruiserEnemy.GetComponent<MobController>().player = player;


                player.GetComponent<PlayerController>().enemies.Add(_MobEnemy.GetComponent<MobController>());
                player.GetComponent<PlayerController>().enemies.Add(_MobEnemy1.GetComponent<MobController>());

                player.GetComponent<PlayerController>().enemies.Add(_RusherEnemy.GetComponent<MobController>());
                player.GetComponent<PlayerController>().enemies.Add(_RusherEnemy1.GetComponent<MobController>());

                player.GetComponent<PlayerController>().enemies.Add(_BruiserEnemy.GetComponent<MobController>());
            }
            cenas--;
        }
    }
}