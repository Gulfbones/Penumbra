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
    private Vector3 desiredScale;
    public AIPath aiPath;
    private Vector3 defaultScale;
    public Vector3 destination;
    private float moveSpeed;





    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
        desiredScale = transform.localScale;
        defaultScale = transform.localScale;
        moveSpeed = aiPath.maxSpeed;



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

        //copied from stalker enemy
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            desiredScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            desiredScale = new Vector3(defaultScale.x * -1, defaultScale.y, defaultScale.z);
        }

    }

    //copied from stalker enemy
    void Flip()
    {
        // Filps the enemies scale to face the destination
        if ((transform.position).x > destination.x && desiredScale.x > 0) // On right side
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);

        }
        if ((transform.position).x < destination.x && desiredScale.x < 0) // On left side
        {
            //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            //desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);

        }
    }
}
