using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RatEnemyScript : MonoBehaviour
{
    private GameObject playerGameObject;
    private PlayerScript playerScript;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*within attacking range of player*/
        if (Mathf.Abs(playerGameObject.transform.position.x - playerGameObject.transform.position.x) <= 2 && Mathf.Abs(playerGameObject.transform.position.y - playerGameObject.transform.position.y) <= 2)
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
        
    }
}
