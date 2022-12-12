using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmEnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject desiredEnemy;

    public GameObject boss;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;

    public float spawnerInterval;
    public float dropInterval;

    public Transform spawner1Pos;
    public Transform spawner2Pos;
    public Transform spawner3Pos;

    private int rand = 0;

    private bool decrease1 = true;
    private bool decrease2 = true;
    private bool decrease3 = true;


    // Start is called before the first frame update
    void Start()
    {
        spawner1Pos = spawner1.transform;
        spawner2Pos = spawner2.transform;
        spawner3Pos = spawner3.transform;
        boss = GameObject.FindGameObjectWithTag("Boss");


        StartCoroutine(spawnEnemy(spawnerInterval, desiredEnemy));
    }

    // Update is called once per frame
    void Update()
    {
        // If the health of the boss drops below a certain point, the interval between spawns decreases
        // Spawners despawn once the boss dies
        if ((boss.GetComponent<FarmBossScript>().bossHealth <= 750) && (decrease1 == true))
        {
            spawnerInterval = (spawnerInterval - dropInterval);
            decrease1 = false;
        }
        if ((boss.GetComponent<FarmBossScript>().bossHealth <= 500) && (decrease2 == true))
        {
            spawnerInterval = (spawnerInterval - dropInterval);
            decrease2 = false;
        }
        if ((boss.GetComponent<FarmBossScript>().bossHealth <= 250) && (decrease3 == true))
        {
            spawnerInterval = (spawnerInterval - dropInterval);
            decrease3 = false;
        }
        if (boss.GetComponent<FarmBossScript>().bossHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Takes a float to determine the interval between spanwns and a game object for the specific enemy being spawned
    // Chooses a random number from 0-2 to determine what spawner the enemy spawns at
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        rand = Random.Range(0, 3);

        if (rand == 0)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawner1Pos);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else if (rand == 1)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawner2Pos);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawner3Pos);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }
}
