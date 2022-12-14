using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBallSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject shadowBall;
    public GameObject spawner;
    public Transform spawnerPos;

    public float spawnerInterval;

    // Start is called before the first frame update
    void Start()
    {
        spawnerPos = spawner.transform;
        StartCoroutine(spawnBall(spawnerInterval, shadowBall));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnBall(float interval, GameObject ball)
    {
        GameObject newBall = Instantiate(shadowBall, spawnerPos);
        yield return new WaitForSeconds(interval);
        StartCoroutine(spawnBall(interval, ball));
    }
    
}
