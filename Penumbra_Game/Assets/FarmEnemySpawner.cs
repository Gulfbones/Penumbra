using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmEnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject desiredEnemy;
    public GameObject desiredEnemy2;
    public GameObject desiredEnemy3;

    public GameObject boss;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;

    public float spawnerInterval;
    public float dropInterval;

    public Transform spawner1Pos;
    public Transform spawner2Pos;
    public Transform spawner3Pos;

    private int randBox = 0;
    private int randEnemy = 0;

    private bool decrease1 = true;
    private bool decrease2 = true;
    private bool decrease3 = true;
    private GameObject enemySpawned;


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
            StopAllCoroutines();
            StartCoroutine(spawnEnemy2(spawnerInterval));
        }
        if ((boss.GetComponent<FarmBossScript>().bossHealth <= 500) && (decrease2 == true))
        {
            spawnerInterval = (spawnerInterval - dropInterval);
            decrease2 = false;
            StopAllCoroutines();
            StartCoroutine(spawnEnemy3(spawnerInterval));
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
        randBox = Random.Range(0, 3);

        if (randBox == 0)
        {
            GameObject newEnemy = Instantiate(enemy, spawner1Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else if (randBox == 1)
        {
            GameObject newEnemy = Instantiate(enemy, spawner2Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else
        {
            GameObject newEnemy = Instantiate(enemy, spawner3Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }

    private IEnumerator spawnEnemy2(float interval)
    {
        randBox = Random.Range(0, 3);
        randEnemy = Random.Range(0, 2);

        // Randomly selects one of two enemies to spawn
        if (randEnemy == 0)
        {
            enemySpawned = desiredEnemy;
        }
        else
        {
            enemySpawned = desiredEnemy2;
        }

        // Randomly selects an area to spawn the enemy, wait, and begin the process again
        if (randBox == 0)
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner1Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy2(interval));
        }
        else if (randBox == 1)
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner2Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy2(interval));
        }
        else
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner3Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy2(interval));
        }
    }
    
    private IEnumerator spawnEnemy3(float interval)
    {
        randBox = Random.Range(0, 3);
        randEnemy = Random.Range(0, 3);

        // Randomly selects one of two enemies to spawn
        if (randEnemy == 0)
        {
            enemySpawned = desiredEnemy;
        }
        else if (randEnemy == 1)
        {
            enemySpawned = desiredEnemy2;
        }
        else
        {
            enemySpawned = desiredEnemy3;
        }

        // Randomly selects an area to spawn the enemy, wait, and begin the process again
        if (randBox == 0)
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner1Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy3(interval));
        }
        else if (randBox == 1)
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner2Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy3(interval));
        }
        else
        {
            GameObject newEnemy = Instantiate(enemySpawned, spawner3Pos);
            yield return new WaitForSeconds(interval);
            StartCoroutine(spawnEnemy3(interval));
        }
    }

}
