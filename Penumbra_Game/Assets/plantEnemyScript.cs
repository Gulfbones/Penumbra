using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class plantEnemyScript : MonoBehaviour
{
    GameObject pcObject;
    PlayerScript pcScript;
    //CircleCollider2D plantAttackCollider;
    CircleCollider2D plantDetectionCollider;
    SpriteRenderer plantSprite;
    bool canHit;
    Vector3 plantEnemyPosition;
    Vector3 playerPosition;
    Vector3 distFromPlayer;
    bool coroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        pcObject = GameObject.FindGameObjectWithTag("Player");
        pcScript = pcObject.GetComponent<PlayerScript>();
        //plantAttackCollider = gameObject.GetComponent<CircleCollider2D>();
        //plantAttackCollider.enabled = false;
        plantDetectionCollider = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
        plantSprite.enabled = false;
        bool canHit = false;
        plantEnemyPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        playerPosition = new Vector3(pcObject.transform.position.x, pcObject.transform.position.y, pcObject.transform.position.z);
        distFromPlayer = new Vector3(0, 0, 0);
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(pcObject.transform.position.x, pcObject.transform.position.y, pcObject.transform.position.z);

    }

    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            UnityEngine.Debug.Log("plantEnemyPosition: " + plantEnemyPosition);
            UnityEngine.Debug.Log("playerPosition: " + playerPosition);
            UnityEngine.Debug.Log("Coroutine Running");
            if (Mathf.Abs(plantEnemyPosition.x) - Mathf.Abs(playerPosition.x) <= 1.5 && Mathf.Abs(plantEnemyPosition.y) - Mathf.Abs(playerPosition.y) <= 1.5)
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
                yield return new WaitForSeconds(2.0f);
                pcScript.setWaxCurrent(pcScript.getWaxCurrent() - 10);
            }
            yield return null;
        }
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
                StartCoroutine(AttackCoroutine());
            }
            UnityEngine.Debug.Log("Coroutine Started");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Play ending animation
            StopCoroutine(AttackCoroutine());
            coroutineRunning = false;
            UnityEngine.Debug.Log("Coroutine Stopped");
            plantSprite.enabled = false;
        }
    }
    //subtract plant position from player position to determine if player is close enough to plant for the plant to attack and hit the player
    //instead of using trigger collider and OnTrigger functions

    //probably can keep trigger collider for plant detection radius collider but use above strategy for plant attack
    //do the position subtracting in a coroutine
    
}
