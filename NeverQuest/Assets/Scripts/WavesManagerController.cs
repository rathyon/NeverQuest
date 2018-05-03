using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManagerController : MonoBehaviour
{

    public int NumberOfWaves;
    public int SpawnDelay;
    public GameObject Player, Mob, Rusher, Bruiser;
    public List<Vector4> SpawnPoints = new List<Vector4>();
    public DoorController[] doorsList;

    public int[] MobsPerWave;
    public int[] RushersPerWave;
    public int[] BruisersPerWave;

    private int CurrentWave = 1;
    private bool IsActionPhase = false;
    private bool AllSpawned = false;
    private int LivingEnemies = 0;

    //keep track of how many have been spawned to know when to stop
    private int MobsSpawned = 0;
    private int RushersSpawned = 0;
    private int BruisersSpawned = 0;

    public void EnemySlain()
    {
        LivingEnemies--;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("SpawnEnemies"); //remember to remove this
    }

    // Update is called once per frame
    void Update()
    {

        if (MobsSpawned >= MobsPerWave[CurrentWave - 1] &&
            RushersSpawned >= RushersPerWave[CurrentWave - 1] &&
            BruisersSpawned >= BruisersPerWave[CurrentWave - 1])
        {
            StopCoroutine("SpawnEnemies");
            AllSpawned = true;
        }

        if (AllSpawned && LivingEnemies <= 0)
        {
            //reset booleans and increment wave...
            AllSpawned = false;
            IsActionPhase = false;
            MobsSpawned = 0;
            RushersSpawned = 0;
            BruisersSpawned = 0;
            CurrentWave++;
            Debug.Log("------------------Jobs Done!");
        }

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);

            // HARDCODED NUMBER OF ENEMY TYPES!!! CHANGE WHEN NUMBER OF TYPES INCREASES!!


            List<int> available = new List<int>();
            if (MobsSpawned < MobsPerWave[CurrentWave - 1])
                available.Add(1);
            if (RushersSpawned < RushersPerWave[CurrentWave - 1])
                available.Add(2);
            if (BruisersSpawned < BruisersPerWave[CurrentWave - 1])
                available.Add(3);

            int spawnType;
            if (available.Count == 0)
                spawnType = -1; //spawn nothing
            else
                spawnType = available[Random.Range(0, available.Count - 1)];

            int spawnPoint = Random.Range(0, SpawnPoints.Count);

            if (spawnType == 1)
            {
                GameObject Spawn = Instantiate(Mob, SpawnPoints[spawnPoint], Quaternion.identity);
                Spawn.GetComponent<MobController>().mobTransportLevel = (int)SpawnPoints[spawnPoint].w;
                Spawn.GetComponent<MobController>().doorsAvaiables = doorsList;
                Spawn.GetComponent<MobController>().player = Player;
                Player.GetComponent<PlayerController>().enemies.Add(Spawn.GetComponent<MobController>());
                MobsSpawned++;
                LivingEnemies++;
            }
            else if (spawnType == 2)
            {
                GameObject Spawn = Instantiate(Rusher, SpawnPoints[spawnPoint], Quaternion.identity);
                Spawn.GetComponent<MobController>().mobTransportLevel = (int)SpawnPoints[spawnPoint].w;
                Spawn.GetComponent<MobController>().doorsAvaiables = doorsList;
                Spawn.GetComponent<MobController>().player = Player;
                Player.GetComponent<PlayerController>().enemies.Add(Spawn.GetComponent<MobController>());
                RushersSpawned++;
                LivingEnemies++;
            }
            else if (spawnType == 3)
            {
                GameObject Spawn = Instantiate(Bruiser, SpawnPoints[spawnPoint], Quaternion.identity);
                Spawn.GetComponent<MobController>().mobTransportLevel = (int)SpawnPoints[spawnPoint].w;
                Spawn.GetComponent<MobController>().doorsAvaiables = doorsList;
                Spawn.GetComponent<MobController>().player = Player;
                Player.GetComponent<PlayerController>().enemies.Add(Spawn.GetComponent<MobController>());
                BruisersSpawned++;
                LivingEnemies++;
            }
        }
    }
}
