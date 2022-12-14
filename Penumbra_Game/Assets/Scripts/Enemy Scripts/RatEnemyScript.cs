using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Pathfinding;

public class RatEnemyScript : MonoBehaviour
{
    private GameObject playerGameObject;
    private PlayerScript playerScript;
    private Animator animator;
    private Vector3 scaleChange;
    private IEnumerator attack;
    private bool coroutineRunning;


    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
        scaleChange = new Vector3 (gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        attack = AttackCoroutine();
        coroutineRunning = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerGameObject.transform.position, 0.1f);
        //gameObject.transform.x = Vector3.MoveTowards();
        //gameObject.transform.x = Vector3.MoveTowards();

        /*within attacking range of player*/
        if (Mathf.Abs(playerGameObject.transform.position.x - gameObject.transform.position.x) <= 2 && Mathf.Abs(playerGameObject.transform.position.y - gameObject.transform.position.y) <= 2)
        {
            animator.SetBool("inAttackRange", true);
            //play attack animation
            //attack the player
            if (!coroutineRunning)
            {
                coroutineRunning = true;
                StartCoroutine(attack);
            }
        }
        else
        {
            animator.SetBool("inAttackRange", false);
            StopCoroutine(attack);
            coroutineRunning = false;

        }

        if (transform.hasChanged)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if (playerGameObject.transform.position.x < gameObject.transform.position.x)
        {
            if (gameObject.transform.localScale.x > 0)
            {
                scaleChange = new Vector3(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                gameObject.transform.localScale = scaleChange;
            }
            else
            {
                scaleChange = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                gameObject.transform.localScale = scaleChange;
            }
        }
        else if (playerGameObject.transform.position.x > gameObject.transform.position.x)
        {
            if (gameObject.transform.localScale.x < 0)
            {
                scaleChange = new Vector3(-1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                gameObject.transform.localScale = scaleChange;
            }
            else
            {
                scaleChange = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                gameObject.transform.localScale = scaleChange;
            }
        }
    }
    public IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.133f);
        while (true)
        {
            yield return new WaitForSeconds(2.15f);
            playerScript.setWaxCurrent(playerScript.getWaxCurrent() - 10);
            yield return null;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Drop Flame") || other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
