using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float waxMax = 160.0f; // This float value represents amount of seconds the candle can burn
    //public float waxCurrent;
    private float standardWaxLost, attackingWaxLost, candleDropWaxLost, hidingWaxLost, attackingGrowSpeed, hidingShrinkSpeed;//, dropFlameWaxCost;
    public float waxCurrent, dropCoolDown, dropCoolDownTimer;

    public PlayerMovementScript playerMovementScript;

    [SerializeField] GameObject lightHitBox;
    [SerializeField] GameObject droppedFlame;
    [SerializeField] Light2D candleLight;
    private float originalLightSize;
    private Vector3 startingLightHitBox, attackingLightHitBox, hidingLightHitBox;

    //bool wasAttacking;
    bool attacking, busy, candleDropping, hidingFlame;
    public GameObject down, up, left, right;
    public GameObject interactUI;
    public SpriteRenderer interactSprite;
    private GameObject deathAnim;

    private AudioSource audioSource;
    public AudioClip clipFlameBig;
    public AudioClip clipFlameGoingOut;
    private bool dead;
    public AudioClip clipGameOver;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();

        waxCurrent = waxMax;
        standardWaxLost = 1.0f;     //.5
        attackingWaxLost = 5.0f;    //5.25
        candleDropWaxLost = 20.0f;
        hidingWaxLost = 0.75f;
        startingLightHitBox = new Vector3(lightHitBox.transform.localScale.x, lightHitBox.transform.localScale.y, lightHitBox.transform.localScale.z);
        attackingLightHitBox = new Vector3(lightHitBox.transform.localScale.x * 2.0f, lightHitBox.transform.localScale.y * 1.5f, lightHitBox.transform.localScale.z);
        hidingLightHitBox = new Vector3(lightHitBox.transform.localScale.x * 0.5f, lightHitBox.transform.localScale.y * 0.67f, lightHitBox.transform.localScale.z);
        attackingGrowSpeed = 15.0f;
        hidingShrinkSpeed = 15.0f;
        originalLightSize = candleLight.pointLightOuterRadius;

        attacking = false;
        busy = false;
        candleDropping = false;
        hidingFlame = false;
        dead = false;

        down = gameObject.transform.GetChild(1).gameObject;
        up = gameObject.transform.GetChild(2).gameObject;
        left = gameObject.transform.GetChild(3).gameObject;
        right = gameObject.transform.GetChild(4).gameObject;
        
        dropCoolDown = 5.0f;        // Time it takes for drop to recharge
        dropCoolDownTimer = 0.0f;   // actual timer drop ability
        //dropFlameWaxCost = 20.0f;

        interactUI = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        interactUI.SetActive(false);
        deathAnim = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        deathAnim.SetActive(false);

        //interactSprite = interactUI.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking();
        hideFlame();
        candleDrop();
        waxMeter();
        if(dead && Input.GetKeyDown(KeyCode.R))
        {
            string name = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(name);//"Sprint_3_03");
        }
    }


    public bool isAttacking()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            audioSource.PlayOneShot(clipFlameBig, 0.25f);
        }
        //Check if player is attacking
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.K)) && !busy && !candleDropping && !hidingFlame) // Is attacking
        {
            attacking = true;
            
            // Play attack animation
            up.GetComponent<Animator>().SetBool("playAttacking", true);
            down.GetComponent<Animator>().SetBool("playAttacking", true);
            left.GetComponent<Animator>().SetBool("playAttacking", true);
            right.GetComponent<Animator>().SetBool("playAttacking", true);

            // Grows light hit box size
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox, attackingGrowSpeed * Time.deltaTime);
            // Grows light size
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize*2, attackingGrowSpeed * Time.deltaTime);
        }
        else // Not attacking
        {
            attacking = false;

            // Stop attack animation
            up.GetComponent<Animator>().SetBool("playAttacking", false);
            down.GetComponent<Animator>().SetBool("playAttacking", false);
            left.GetComponent<Animator>().SetBool("playAttacking", false);
            right.GetComponent<Animator>().SetBool("playAttacking", false);
            if (!hidingFlame)
            {
                // Shrinks light hit box size
                lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, startingLightHitBox, attackingGrowSpeed * 2 * Time.deltaTime);
                // Shrinks light size
                candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, attackingGrowSpeed * 2 * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            audioSource.Stop();
            //GetComponent<AudioSource>();
        }
        return attacking;
    }

    public bool candleDrop()
    {
        // While drop timer is not less than 0
        if (dropCoolDownTimer >= -0.5f)
        {
            dropCoolDownTimer -= 1.0f * Time.deltaTime;
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.L)) && !busy && !attacking && !hidingFlame && (dropCoolDownTimer <= 0.0f))
        {
            // Creates drop flame object
            Instantiate(droppedFlame, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Quaternion.identity);
            waxCurrent -= candleDropWaxLost;// dropFlameWaxCost;
            dropCoolDownTimer = dropCoolDown; // Sets drop timer to 5

            // Playing Flame drop Animation
            up.GetComponent<Animator>().SetTrigger("triggerFlamePlace");
            down.GetComponent<Animator>().SetTrigger("triggerFlamePlace");
            left.GetComponent<Animator>().SetTrigger("triggerFlamePlace");
            right.GetComponent<Animator>().SetTrigger("triggerFlamePlace");
        }
        else
        {
            candleDropping = false;
        }
        return candleDropping;
    }

    public bool hideFlame()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.J)) && !busy && !attacking && !candleDropping)
        {
            hidingFlame = true;

            //play hide flame animation

            // Shrinks light hit box size
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, hidingLightHitBox, hidingShrinkSpeed * Time.deltaTime);
            // Shrinks light size
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize * 0.5f, hidingShrinkSpeed * Time.deltaTime);
            UnityEngine.Debug.Log("lightHitBox.transform.localScale: " + lightHitBox.transform.localScale);
            UnityEngine.Debug.Log("candleLight.pointLightOuterRadius: " + candleLight.pointLightOuterRadius);

        }
        else
        {
            hidingFlame = false;
            if (!attacking)
            {
                // Grows light hit box size
                lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, startingLightHitBox, hidingShrinkSpeed * 2 * Time.deltaTime);
                // Grows light size
                candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, hidingShrinkSpeed * 2 * Time.deltaTime);
            }
            UnityEngine.Debug.Log("lightHitBox.transform.localScale: " + lightHitBox.transform.localScale);
            UnityEngine.Debug.Log("candleLight.pointLightOuterRadius: " + candleLight.pointLightOuterRadius);

        }

        return hidingFlame;
    }

    public void waxMeter()
    {
        //Wax meter logic
        if (candleDropping)
        {
            waxCurrent -= candleDropWaxLost;
        }
        else if (attacking)
        {
            waxCurrent -= attackingWaxLost * Time.deltaTime;
        }
        else if(hidingFlame)
        {
            waxCurrent -= hidingWaxLost * Time.deltaTime;
        }
        else
        {
            waxCurrent -= standardWaxLost * Time.deltaTime;
        }
        // Makes sure current player wax doesn't go over maximum wax amount
        if (waxCurrent > waxMax)
        {
            waxCurrent = waxMax;
        }
        // No wax left
        if (waxCurrent <= 0 && !dead)
        {
            StartCoroutine(DieCoroutine());
        }
        //{
        //    dead = true;
        //    deathAnim.SetActive(true);
        //    audioSource.PlayOneShot(clipFlameGoingOut, 0.8f);
        //    GameObject.Find("Game Music").SetActive(false);
        //    //audioSource.PlayScheduled()
        //    //UnityEngine.Debug.Log("Game Over");
        //    //Destroy(gameObject); // Destroys player game object
        //}
        if (dead)
        {
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox * 0, attackingGrowSpeed * 3 * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, 0, attackingGrowSpeed * 3 * Time.deltaTime);
        }
    }

    public IEnumerator DieCoroutine()
    {
        dead = true;
        deathAnim.SetActive(true);
        audioSource.PlayOneShot(clipFlameGoingOut, 0.6f);
        GameObject.Find("Game Music").SetActive(false);
        //moveSpeed = stalkMoveSpeed; // set move speed to stalking
        //var dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        //destination = Randomize(dist / 2, dist); // Randomized Distance
        //Flip();
        yield return new WaitForSeconds(5);
        audioSource.PlayOneShot(clipGameOver, 0.6f);
    }

    /* 
     * addRate: rate at which wax meter increases.
     * Defaults to 10 
     */
    public void addWax(float addRate = 10.0f)
    {
        waxCurrent += (addRate) * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Light"))
        {
            //UnityEngine.Debug.Log("collider: " + other);
        }
        // If player touches a hazard
        if (other.CompareTag("Hazard")) {
            waxCurrent -= 20.0f;
            if(waxCurrent <= 0)
            {
                //?
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If enemy gets too close to player
        if (other.gameObject.CompareTag("Enemy"))
        {
            waxCurrent -= 10.0f;
        }
        if (other.CompareTag("Interactable") || other.CompareTag("Lantern"))
        {
            interactUI.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") || other.CompareTag("Lantern") || other.CompareTag("Untagged"))
        {
            interactUI.SetActive(false);
        }
    }
    
    
    public float getWaxMax()
    {
        return waxMax;
    }
    public float getWaxCurrent()
    {
        return waxCurrent;
    }
    public void setWaxCurrent(float input)
    {
        waxCurrent = input;
    }
    public float getStandardWaxLost()
    {
        return standardWaxLost;
    }
    public float getAttackingWaxLost()
    {
        return attackingWaxLost;
    }
    public bool getAttacking()
    {
        return attacking;
    }
    public bool getBusy()
    {
        return busy;
    }
    
}
