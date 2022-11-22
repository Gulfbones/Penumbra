using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    public float waxMax = 160.0f; // This float value represents amount of seconds the candle can burn
    //public float waxCurrent;
    private float standardWaxLost, attackingWaxLost, candleDropWaxLost, attackingGrowSpeed, dropFlameWaxCost;
    public float waxCurrent, dropCoolDown, dropCoolDownTimer;

    public PlayerMovementScript playerMovementScript;

    [SerializeField] GameObject lightHitBox;
    [SerializeField] GameObject droppedFlame;
    [SerializeField] Light2D candleLight;
    private float originalLightSize;
    private Vector3 startingLightHitBox, attackingLightHitBox;

    //bool wasAttacking;
    bool attacking, busy, candleDropping;
    public GameObject down, up, left, right;
    public GameObject mainCamera;
    public GameObject inRangeInteractableUI;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();

        waxCurrent = waxMax;
        standardWaxLost = 1.0f;     //.5
        attackingWaxLost = 5.0f;    //5.25
        candleDropWaxLost = 20.0f;
        startingLightHitBox = new Vector3(lightHitBox.transform.localScale.x, lightHitBox.transform.localScale.y, lightHitBox.transform.localScale.z);
        attackingLightHitBox = new Vector3(lightHitBox.transform.localScale.x*2.0f, lightHitBox.transform.localScale.y*1.5f, lightHitBox.transform.localScale.z);
        attackingGrowSpeed = 15.0f;
        originalLightSize = candleLight.pointLightOuterRadius;

        attacking = false;
        busy = false;
        candleDropping = false;

        down = gameObject.transform.GetChild(1).gameObject;
        up = gameObject.transform.GetChild(2).gameObject;
        left = gameObject.transform.GetChild(3).gameObject;
        right = gameObject.transform.GetChild(4).gameObject;
        
        dropCoolDown = 5.0f;        // Time it takes for drop to recharge
        dropCoolDownTimer = 0.0f;   // actual timer drop ability
        dropFlameWaxCost = 20.0f;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        inRangeInteractableUI = mainCamera.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking();
        candleDrop();
        waxMeter();
    }
    
    public bool isAttacking()
    {
        //Check if play is attacking
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.K)) && !busy && !candleDropping) // Is attacking
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
            
            // Shrinks light hit box size
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, startingLightHitBox, attackingGrowSpeed * 2 * Time.deltaTime);
            // Shrinks light size
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, attackingGrowSpeed * 2 * Time.deltaTime);
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
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.L)) && !busy && !attacking && (dropCoolDownTimer <= 0.0f))
        {
            // Creates drop flame object
            Instantiate(droppedFlame, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Quaternion.identity);
            waxCurrent -= dropFlameWaxCost;
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

    public void waxMeter()
    {
        //Wax meter logic
        if (candleDropping)
        {
            waxCurrent -= candleDropWaxLost;
        }
        else if (!attacking)
        {
            waxCurrent -= standardWaxLost * Time.deltaTime;
        }
        else
        {
            waxCurrent -= attackingWaxLost * Time.deltaTime;
        }
        // Makes sure current player wax doesn't go over maximum wax amount
        if (waxCurrent > waxMax)
        {
            waxCurrent = waxMax;
        }
        // No wax left
        if (waxCurrent <= 0)
        {
            //UnityEngine.Debug.Log("Game Over");
            //Destroy(gameObject); // Destroys player game object
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox * 0, attackingGrowSpeed * 3 * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, 0, attackingGrowSpeed * 3 * Time.deltaTime);
        }
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
        if (other.CompareTag("Interactable"))
        {
            inRangeInteractableUI.GetComponent<SpriteRenderer>().enabled = true;
            //UnityEngine.Debug.Log("sprite enabled: " + inRangeInteractableUI.GetComponent<SpriteRenderer>().enabled);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If enemy gets too close to player
        if (other.gameObject.CompareTag("Enemy"))
        {
            waxCurrent -= 10.0f;
        }

    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            inRangeInteractableUI.GetComponent<SpriteRenderer>().enabled = false;
            UnityEngine.Debug.Log("sprite enabled: " + inRangeInteractableUI.GetComponent<SpriteRenderer>().enabled);
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
