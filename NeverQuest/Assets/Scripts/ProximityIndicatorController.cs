using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityIndicatorController : MonoBehaviour
{

    public GameObject indicatorPrefab;
    public float radius;

    private List<GameObject> indicators = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    // THE RADIUS OF THE INDICATOR IS 3.8, USE THIS VALUE!
    //private float radius = 3.0f;

    private void AddIndicator(GameObject enemy)
    {
        Vector3 diff = enemy.transform.position - transform.position;
        diff.Normalize();
        Vector3 pos = (radius * diff) + transform.position;
        var new_indicator = Instantiate(indicatorPrefab, pos, Quaternion.identity);
        new_indicator.transform.parent = gameObject.transform;
        indicators.Add(new_indicator);
    }

    private void UpdateIndicator(GameObject enemy, GameObject indicator)
    {
        Vector3 diff = enemy.transform.position - transform.position;
        diff.Normalize();
        indicator.transform.position = (radius * diff) + transform.position;
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Mob");
        Debug.Log("Proximity Indicator Start: " + enemiesArray.Length + " enemies found.");
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
            AddIndicator(enemy);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Mob");

        /*
        // Add gameobject if its not in the list
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            bool contains = false;
            if (enemies.Count <= 0)
            {
                enemies.Add(enemiesArray[i]);
                AddIndicator(enemiesArray[i]);
            }
            else
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (GameObject.ReferenceEquals(enemiesArray[i], enemies[j]))
                    {
                        contains = true;
                    }

                    if (!contains)
                    {
                        enemies.Add(enemiesArray[i]);
                        AddIndicator(enemiesArray[i]);
                    }
                }
            }
        }
        */

        foreach (GameObject enemy in enemiesArray)
        {
            //check if enemy is in the list
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                AddIndicator(enemy);
            }
        }

        //Update Indicators
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
                UpdateIndicator(enemies[i], indicators[i]);
            else
                Destroy(indicators[i]);
        }

    }

    public void removeEnemy(GameObject enemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (GameObject.ReferenceEquals(enemies[i], enemy))
            {
                Destroy(indicators[i]);
                indicators.RemoveAt(i);
                enemies.RemoveAt(i);
            }
        }
    }
}


