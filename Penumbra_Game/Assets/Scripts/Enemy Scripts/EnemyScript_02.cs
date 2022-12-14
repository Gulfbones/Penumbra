using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random; // tell script we are using unitys engines
//using Pathfinding;

public class EnemyScript_02 : MonoBehaviour
{
    
    public Vector3 destination;
    public float timer;
    public int attackTimer = 10;
    private Vector3 defaultScale;
    public Vector3 desiredScale;
    public Vector3 desiredEyeScale;
    public Vector3 closedEyeScale;
    public Vector3 originalEyeScale;
    private float moveSpeed;
    public float stalkMoveSpeed = 12.0f;
    public float attackMoveSpeed = 10.0f;
    public float fleeMoveSpeed = -15.0f;
    public float triggerDistance = 15.0f;
    private GameObject eyesGameObject;
    private GameObject Candle;
    private AudioSource Bone_2;
    private Animator animator;
    private bool fleeCoroutineRunning;
    public bool sleeping = false;
    public float dist;
    private AudioSource audioSource;
    //public AudioClip clipDrone;
    public float droneVol;
    public AudioClip clipAttack;
    public enum Enemy_State
    {
        STALKING,
        ATTACKING,
        FLEEING,
        SLEEPING,
        WAKING
    };
    private Enemy_State state;
    //public AIPath aiPath;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        droneVol = 0.5f;
        Candle = GameObject.Find("Candle");
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        eyesGameObject = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0)
            .gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Bone_2 = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0)
            .gameObject.GetComponent<AudioSource>();
        fleeCoroutineRunning = false;
        if (sleeping)
        {
            droneVol = 0.1f;
            state = Enemy_State.SLEEPING;
            animator.SetBool("Sleeping", true);
            eyesGameObject.GetComponent<Blinker>().enabled = false;
            //eyesGameObject.transform.localScale = new Vector3(1, 0.25f,1); // Sets eye scale to halfish
        }
        else
        {
            state = Enemy_State.STALKING;
            droneVol = 0.5f;
        }
        timer = 0;
        defaultScale = transform.localScale;
        desiredScale = defaultScale;
        originalEyeScale = eyesGameObject.transform.localScale;
        closedEyeScale = new Vector3(originalEyeScale.x, 0.05f, originalEyeScale.z);
        Bone_2.volume = droneVol;

    }
    
    // Update is called once per frame
    void Update()
    {
        //if(gameObject.)
        switch (state)
        {
            case Enemy_State.STALKING:
                timer += Time.smoothDeltaTime;
                int timerRounded = (int)(Math.Round(timer));
                if (timer%2 >= 0 && timer%2 <=0.1f)
                {
                    SetStalk();
                    
                }
                dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                if (dist < 10.0f || timerRounded > attackTimer) // If distance is less than amount, or timer > 10, attack
                {
                    state = Enemy_State.ATTACKING;
                    timer = 0;
                }
                break;
            
            case Enemy_State.ATTACKING:
                Attack();
                break;

            case Enemy_State.FLEEING:
                if (!fleeCoroutineRunning)
                {
                    StartCoroutine(FleeCoroutine());
                }
                break;
            case Enemy_State.SLEEPING:
                //desiredEyeScale = new Vector3(originalEyeScale.x,0.25f, originalEyeScale.z);
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("stalker waking") && animator.GetFloat("Waking speed") < 0.0f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
                {
                    Debug.Log("gong to sleep ");
                    animator.Play("stalkerSleepingIdle");

                    //sleeping = true;
                }
                eyesGameObject.transform.localScale = Vector3.MoveTowards(eyesGameObject.transform.localScale, closedEyeScale, 0.25f * Time.deltaTime);
                //Vector3.ler
                //Debug.Log("sleeping");
                //eyesGameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                // DO NOTHING
                break;
            case Enemy_State.WAKING:
                dist = Vector3.Distance(transform.position, Candle.transform.position);

                //desiredEyeScale = new Vector3(eyesGameObject.transform.localScale.x, eyesGameObject.transform.localScale.y, eyesGameObject.transform.localScale.z);
                if (dist < triggerDistance) // If within distance, begin waking up
                {
                    animator.SetFloat("Waking speed", (triggerDistance - dist) / triggerDistance);
                    sleeping = false;
                    eyesGameObject.transform.localScale = Vector3.MoveTowards(eyesGameObject.transform.localScale, originalEyeScale, ((((triggerDistance - dist) / triggerDistance)) * 1.0f) * Time.deltaTime);
                }
                else // If not within distance, begin going back to sleep
                {
                    animator.SetFloat("Waking speed", -(0.25f));
                    sleeping = true;
                    //eyesGameObject.transform.localScale = Vector3.MoveTowards(eyesGameObject.transform.localScale, closedEyeScale, 0.5f * Time.deltaTime);
                }
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("PenubraStalkerIdle")) // if animation moved to idle, wake up
                {
                    sleeping = false;
                    animator.SetFloat("Waking speed", 1);
                    state = Enemy_State.ATTACKING;
                    Bone_2.volume = 0.5f;
                    attackTimer = 4; // more agressive on wake;
                    eyesGameObject.GetComponent<Blinker>().enabled = true;
                    //eyesGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                }
                animator.SetBool("Sleeping", sleeping);
                //eyesGameObject.transform.localScale = Vector3.Lerp(originalEyeScale, originalEyeScale, 0.01f);
                //float eyeVal = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime / animator.GetCurrentAnimatorStateInfo(0).length);//animator.GetFloat("Waking speed");//Mathf.MoveTowards(, 0,  3 * Time.deltaTime);
                //Debug.Log("waking");
                //eyesGameObject.GetComponent<SpriteRenderer>().color = new Color(eyeVal, eyeVal, eyeVal);
                break;
            default:
                break;
        };

        // Moves the enemy toward the destination over time
        //GameObject.Find("Dev_Square").gameObject.transform.position = destination; // Moves a square to where the desired location is
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(desiredScale.x,desiredScale.y, desiredScale.z), 30.0f * Time.deltaTime);
    }
    public IEnumerator StalkCoroutine()
    {
        moveSpeed = stalkMoveSpeed; // set move speed to stalking
        var dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        destination = Randomize(dist / 2, dist); // Randomized Distance
        Flip();
        yield return null;
    }

    public IEnumerator FleeCoroutine()
    {
        fleeCoroutineRunning = true;
        moveSpeed = fleeMoveSpeed;
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Correctly set flip for fleeing specificlly
        if ((transform.position).x > destination.x && desiredScale.x < 0) // On right side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if ((transform.position).x < destination.x && desiredScale.x > 0) // On left side
        {
            desiredScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        //playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
        yield return new WaitForSeconds(5);
        state = Enemy_State.STALKING;
        Teleport();
        fleeCoroutineRunning = false;
    }

    // Flips the enemies scale to face the destination
    void Flip()
    {
        if ((transform.position).x > destination.x && desiredScale.x > 0) // On right side
        {
            desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);
        }
        if ((transform.position).x < destination.x && desiredScale.x < 0) // On left side
        {
            desiredScale = new Vector3(desiredScale.x * -1, desiredScale.y, desiredScale.z);
        }
    }
    
    // Returns a random vector3 based on given minimum distance and maximum
    public Vector3 Randomize(float min, float max)
    {
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
    
    // Sets postion to a random spot
    public void Teleport()
    {
        transform.position = Randomize(20.0f, 30.0f);
        desiredScale = new Vector3(3, 3, 0);
        destination = transform.position;
    }
    
    //Sets random postion to move to
    public void SetStalk()
    {
        moveSpeed = stalkMoveSpeed; // set move speed to stalking
        var dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position); 
        destination = Randomize(dist/2, dist); // Randomized Distance
        Flip();
    }
    /*
    */
    // Sets postion to move to player
    public void Attack()
    {
        moveSpeed = attackMoveSpeed;
        destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Flip();
    }
    /*
     // Sets postion to move away from player
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
            //  Debug.Log("Contact Flee Light");
            if (state != Enemy_State.SLEEPING) //if not sleeping
            {
                state = Enemy_State.FLEEING;
            }
            else
            {
                state = Enemy_State.WAKING;
                animator.SetFloat("Waking speed", -1);
                sleeping = false;
                animator.SetBool("Sleeping", sleeping);
            }
        }

        if (!sleeping && other.gameObject.tag == "Player" && state != Enemy_State.FLEEING)//&& timer > 0)
        {
            animator.SetTrigger("Attack");
            audioSource.PlayOneShot(clipAttack,0.8f);
            state = Enemy_State.FLEEING;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Light")
        {
            //  Debug.Log("Contact Flee Light");
            if (state == Enemy_State.WAKING)
            {
                state = Enemy_State.SLEEPING;
                animator.SetFloat("Waking speed", -(0.25f));
                sleeping = true;
                animator.SetBool("Sleeping", sleeping);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MinecartWall" || collision.gameObject.tag == "Wall" )
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    /*
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
