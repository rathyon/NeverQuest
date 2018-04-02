using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawner : MonoBehaviour{

	public GameObject[] enemies;

	public Vector3 spawnValues;
	public float spawnMostWait, spawnLeastWait;
	public int startWait, spawnWait, spawnTimer, playTime, seconds;
	public bool stop;
	public Text textSpawnTime;
	public int[] arrayT ;

	int randEnemy;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(PlayTimer());
		StartCoroutine(waitSpawner());
		textSpawnTime.text = "Next wave in: " + spawnTimer.ToString() + " seconds!";
		spawnWait = spawnTimer;
	}

	// Update is called once per frame
	void Update ()
	{
		textSpawnTime.text = "Next wave in: " + spawnTimer.ToString() + " seconds!";
	}

	IEnumerator PlayTimer(){
		while (true) {
			yield return new WaitForSeconds (1);
			playTime = 1;
			seconds = (playTime % 60);
			spawnTimer -= seconds;

			if (spawnTimer <= 0)
				spawnTimer = spawnWait;
		}
	}

	IEnumerator waitSpawner(){
		while (true) {
			yield return new WaitForSeconds (spawnTimer);

			for (int count = 0; count < enemies.Length; count++) {
				//randEnemy = Random.Range(0,1);

				Vector3 spawnPosition = new Vector3 (arrayT[Random.Range (0, 2)], -12, 0);

				Instantiate (enemies [count], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

				yield return new WaitForSeconds (1);
			}
		}
	}
}

