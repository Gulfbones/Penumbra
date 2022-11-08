using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class plantEnemyScript : MonoBehaviour
{

    PlayerScript pcScript;
    CircleCollider2D plantAttackCollider;
    CircleCollider2D plantDetectionCollider;
    SpriteRenderer plantSprite;

    // Start is called before the first frame update
    void Start()
    {
        pcScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            UnityEngine.Debug.Log("pcScript: " + pcScript);
        plantAttackCollider = gameObject.GetComponent<CircleCollider2D>();
            UnityEngine.Debug.Log("plantAttackCollider: " + plantAttackCollider);
        plantAttackCollider.enabled = false;
            UnityEngine.Debug.Log("plantAttackCollider.enabled: " + plantAttackCollider.enabled);
        plantDetectionCollider = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
            UnityEngine.Debug.Log("plantDetectionCollider" + plantDetectionCollider);
        plantSprite = gameObject.GetComponent<SpriteRenderer>();
            UnityEngine.Debug.Log("plantSprite: " + plantSprite);
        plantSprite.enabled = false;
            UnityEngine.Debug.Log("plantSprite.enabled: " + plantSprite.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.Log("plantAttackCollider.enabled: " + plantAttackCollider.enabled);
        UnityEngine.Debug.Log("plantSprite.enabled: " + plantSprite.enabled);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player gets within the plant's range
        if (other.gameObject.CompareTag("Player"))
        {
            //Play starting animation
        }
    }
    //subtract plant position from player position to determine if player is close enough to plant for the plant to attack and hit the player
    //instead of using trigger collider and OnTrigger functions
    //probably can keep trigger collider for plant detection radius collider but use above strategy for plant attack
    //do the position subtracting in a coroutine
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !plantAttackCollider.enabled)
        {
            //Play attack animation
            plantAttackCollider.enabled = true;
            plantSprite.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Play ending animation
            if (plantAttackCollider.enabled)
            {
                plantAttackCollider.enabled = false;
            }
            plantSprite.enabled = false;
        }
    }

}
