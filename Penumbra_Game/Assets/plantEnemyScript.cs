using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class plantEnemyScript : MonoBehaviour
{
    GameObject pcObject;
    PlayerScript pcScript;
    CircleCollider2D plantDetectionCollider;
    SpriteRenderer plantSprite;
    bool canHit;
    Vector3 plantEnemyPosition;
    Vector3 playerPosition;
    //Vector3 distFromPlayer;
    bool coroutineRunning;
    float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        pcObject = GameObject.FindGameObjectWithTag("Player");
        pcScript = pcObject.GetComponent<PlayerScript>();
        plantDetectionCollider = gameObject.GetComponent<CircleCollider2D>();
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
        plantSprite.enabled = false;
        canHit = false;
        plantEnemyPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        playerPosition = new Vector3(pcObject.transform.position.x, pcObject.transform.position.y, pcObject.transform.position.z);
        //distFromPlayer = new Vector3(0, 0, 0);
        coroutineRunning = false;
        attackRange = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(pcObject.transform.position.x, pcObject.transform.position.y, pcObject.transform.position.z);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player gets within the plant's range
        if (other.gameObject.CompareTag("Player"))
        {
            //Play starting animation
            plantSprite.enabled = true;
            if (!coroutineRunning)
            {
                coroutineRunning = true;
                UnityEngine.Debug.Log("Coroutine Started");
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    
    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            plantSprite.enabled = true;
            if(!coroutineRunning)
            {
                coroutineRunning=true;
                UnityEngine.Debug.Log("Coroutine Started");
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    */

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Play ending animation
            coroutineRunning = false;
            StopCoroutine(AttackCoroutine());
            UnityEngine.Debug.Log("Coroutine Stopped");
            plantSprite.enabled = false;
        }
    }

    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            UnityEngine.Debug.Log("plantEnemyPosition: " + plantEnemyPosition);
            UnityEngine.Debug.Log("playerPosition: " + playerPosition);
            UnityEngine.Debug.Log("Coroutine Running");
            if (Mathf.Abs(plantEnemyPosition.x - playerPosition.x) <= attackRange && Mathf.Abs(plantEnemyPosition.y - playerPosition.y) <= attackRange)
            {
                canHit = true;
            }
            else
            {
                canHit = false;
            }
            UnityEngine.Debug.Log("canHit: " + canHit);
            if (canHit)
            {
                //Play attack animation
                //WaitForSeconds (until attack animation ends)
                UnityEngine.Debug.Log("Waiting 2 seconds");
                yield return new WaitForSeconds(2.0f);

                if (Mathf.Abs(plantEnemyPosition.x - playerPosition.x) <= attackRange && Mathf.Abs(plantEnemyPosition.y - playerPosition.y) <= attackRange)
                {
                    canHit = true;
                }
                else
                {
                    canHit = false;
                }
                if (canHit)
                {
                    pcScript.setWaxCurrent(pcScript.getWaxCurrent() - 10);
                    UnityEngine.Debug.Log("Damaged Player");
                }
            }
            yield return null;
        }
    }

    //subtract plant position from player position to determine if player is close enough to plant for the plant to attack and hit the player
    //instead of using trigger collider and OnTrigger functions

    //probably can keep trigger collider for plant detection radius collider but use above strategy for plant attack
    //do the position subtracting in a coroutine
    
}
