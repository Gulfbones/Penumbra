using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random; // tell script we are using unitys engines
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    
    public Vector3 destination;
    //public int timer = 0;
    //public int attackTimer = 300;
    private Vector3 defaultScale;
    private Vector3 desiredScale;
    private float moveSpeed;
    //public float stalkMoveSpeed = 12.0f;
    //public float attackMoveSpeed = 10.0f;
    //public float fleeMoveSpeed = -16.0f;
    private GameObject OriginalGameObject;
    private Animator animator;

    public AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
        desiredScale = transform.localScale;
        moveSpeed = aiPath.maxSpeed;
        OriginalGameObject = gameObject;
        animator = OriginalGameObject.transform.GetChild(0).GetComponent<Animator>();
        //aiPath.destination.Set();
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            desiredScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            desiredScale = new Vector3(defaultScale.x * -1, defaultScale.y, defaultScale.z);
        }
        //aiPath
        /*
        // When time hits -100, change 
        if (timer == -100)
        {
            Flee();
        }
        if (timer >= attackTimer)
        {
            Attack();
        }
        */
        // Moves the enemy toward the destination over time
        //transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(desiredScale.x,desiredScale.y, desiredScale.z), 20.0f * Time.deltaTime);
    }

    // Run like Update, but independent of frame rate: 50 tps
    private void FixedUpdate()
    {
        /*
        timer++;
        // Teleport enemy
        if(timer == 0)
        {
            //Debug.Log("Timer Teleport");
            Teleport();
        }
        // Calls stalk every 2 seconds
        if (timer > 0 && timer < attackTimer && timer % 100 == 0)
        { // 0 seconds
            //Debug.Log("Timer Tick Stalk");
            Stalk();
        }
        */
    }

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
    /*
     * returns a random vector3 based on given minimum distance and maximum
     */
    public Vector3 Randomize(float min, float max)
    {
        //Vector3 loc;
        //originalPos = transform.position;
        float x = Random.Range(min, max) * PositiveOrNegative(); 
        float y = Random.Range(min, max) * PositiveOrNegative();
        var vectorDistance = new Vector3(x , y , 0);
        return GameObject.FindGameObjectWithTag("Player").transform.position + vectorDistance;
    }

    // Returns a Positve or a Negative number randomly
    public float PositiveOrNegative()
    {
        if (Random.value >= 0.5)
        {
            return 1.0f;
        }
        return -1.0f;
    }

    
    /*
     * Sets postion to a random spot
     */
    public void Teleport()
    {
        transform.position = Randomize(20.0f, 30.0f);
        desiredScale = new Vector3(3, 3, 0);
        destination = transform.position;
    }

    /*
     * Sets random postion to move to
    public void Stalk()
    {
        moveSpeed = stalkMoveSpeed; // set move speed to stalking
        var dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position); 
        destination = Randomize(dist/2, dist); // Randomized Distance
        if (dist < 15.0f)
        {
            timer = attackTimer;
        }
        Flip();
    }
     */

    /*
     * Sets postion to move to player
    public void Attack()
    {
        moveSpeed = attackMoveSpeed;
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Flip();
    }
     */

    /*
     * Sets postion to move away from player
    public void Flee()
    {
        moveSpeed = fleeMoveSpeed;
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Correctly set flip for fleeing
        if ((transform.position).x > destination.x && desiredScale.x < 0) // On right side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
        if ((transform.position).x < destination.x && desiredScale.x > 0) // On left side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
    }
    
     */

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name + "Trigger");
        if (other.gameObject.tag == "Light")
        {
            //aiPath.
            //  Debug.Log("Contact Flee Light");
            //timer = -100;

        }

        if (other.gameObject.tag == "Player" )//&& timer > 0)
        {
            animator.SetTrigger("Attack");
            Teleport();
            // Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!Enemy Contacted 2 Player!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //timer = -125;

        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "MinecartWall" || collision.gameObject.tag == "Wall" )
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name + "Collision");
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("Attack");
            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!Enemy Contacted 1 Player!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //timer = -125;

        }
    }
    */
}
