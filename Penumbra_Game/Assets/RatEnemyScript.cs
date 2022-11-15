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


    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
        scaleChange = new Vector3 (gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        /*within attacking range of player*/
        if (Mathf.Abs(playerGameObject.transform.position.x - gameObject.transform.position.x) <= 2 && Mathf.Abs(playerGameObject.transform.position.y - gameObject.transform.position.y) <= 2)
        {
            animator.SetBool("inAttackRange", true);
            //play attack animation
            //attack the player
        }
        else
        {
            animator.SetBool("inAttackRange", false);
        }

        if(transform.hasChanged)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        /*if (playerGameObject.transform.position.x > gameObject.transform.position.x || playerGameObject.transform.localScale.x < 0)
        {
            scaleChange = new Vector3 (-1*gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            gameObject.transform.localScale = scaleChange;
        }
        else if (playerGameObject.transform.position.x < gameObject.transform.position.x || playerGameObject.transform.localScale.x > 0)
        {
            scaleChange = new Vector3 (gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            gameObject.transform.localScale = scaleChange;
        }*/
    }

}
