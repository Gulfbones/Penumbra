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
    float waxMax = 160.0f; // This float value represents amount of seconds the candle can burn
    float waxCurrent;
    float standardWaxLost;
    float attackingWaxLost;
    float candleDropWaxLost;

    public PlayerMovementScript playerMovementScript;

    [SerializeField] GameObject lightHitBox;
    [SerializeField] Light2D candleLight;
    float originalLightSize;
    //GameObject lightHitBox;
    Vector3 startingLightHitBox;
    Vector3 attackingLightHitBox;
    float attackingGrowSpeed;

    //bool wasAttacking;
    bool attacking;
    bool busy;
    bool candleDropping;
    //bool canInteractFountain;
    //bool canInteractLantern;
    public GameObject down;
    public GameObject up;
    public GameObject left;
    public GameObject right;
    //public MeshRenderer currentDirection;

    public GameObject dropFlame;
    public Light2D dropFlameLight;
    public SpriteRenderer dropFlameSprite;

    IEnumerator dropFlameWaxCoroutine;
    float dropFlameWax;

    bool coroutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
        //lightHitBox = GameObject.Find("Light_Hitbox");
        waxCurrent = waxMax;
        standardWaxLost = 1.0f; //.5
        attackingWaxLost = 5.0f; //5.25
        candleDropWaxLost = 20.0f;
        startingLightHitBox = new Vector3(lightHitBox.transform.localScale.x, lightHitBox.transform.localScale.y, lightHitBox.transform.localScale.z);//new Vector(lightHitBox.transform.localPosition.x, lightHitBox.transform, lightHitBox.transform);
        attackingLightHitBox = new Vector3(lightHitBox.transform.localScale.x*2.0f, lightHitBox.transform.localScale.y*1.5f, lightHitBox.transform.localScale.z);//new Vector3(1.0f,1.0f,0.0f);
        attackingGrowSpeed = 15.0f;
        originalLightSize = candleLight.pointLightOuterRadius;

        //wasAttacking = false;
        attacking = false;
        busy = false;
        candleDropping = false;

        down = gameObject.transform.GetChild(1).gameObject;//.GetComponent<MeshRenderer>();
        up = gameObject.transform.GetChild(2).gameObject;//.GetComponent<MeshRenderer>();
        left = gameObject.transform.GetChild(3).gameObject;//.GetComponent<MeshRenderer>();
        right = gameObject.transform.GetChild(4).gameObject;//.GetComponent<MeshRenderer>();
        //currentDirection = right;

        dropFlame = GameObject.FindGameObjectWithTag("Drop Flame");
        dropFlameLight = dropFlame.GetComponent<Light2D>();
        dropFlameSprite = dropFlame.GetComponent<SpriteRenderer> ();
        dropFlameLight.enabled = false;
        dropFlameSprite.enabled = false;

        dropFlameWaxCoroutine = dropFlameCoroutine();
        dropFlameWax = 20.0f;
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //currentDirection = playerMovementScript.getCurrentDirection();
        isAttacking();
        candleDrop();
        waxMeter();
        //UnityEngine.Debug.Log("dropFlameLight.enabled: " + dropFlameLight.enabled);
        //UnityEngine.Debug.Log("dropFlameSprite.enabled: " + dropFlameSprite.enabled);
        //UnityEngine.Debug.Log("dropFlame position: " + dropFlame.transform.position);


    }


    public bool isAttacking()
    {
        //UnityEngine.Debug.Log(currentDirection.GetComponent<Animator>().GetBool("playAttacking"));
        //UnityEngine.Debug.Log("currentDirection:" + currentDirection.transform.name);
        //Check if pc is attacking
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.K) && !busy && !candleDropping)
        {
            
            //play starting attack animation
            up.GetComponent<Animator>().SetBool("playAttacking", true);
            down.GetComponent<Animator>().SetBool("playAttacking", true);
            left.GetComponent<Animator>().SetBool("playAttacking", true);
            right.GetComponent<Animator>().SetBool("playAttacking", true);
            
            //play middle attack animation
            //currentDirection.GetComponent<Animator>().SetBool("playAttack", false);

            attacking = true;
            //wasAttacking = true;
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox, attackingGrowSpeed * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize*2, attackingGrowSpeed * Time.deltaTime);
        }
        else
        {
            up.GetComponent<Animator>().SetBool("playAttacking", false);
            down.GetComponent<Animator>().SetBool("playAttacking", false);
            left.GetComponent<Animator>().SetBool("playAttacking", false);
            right.GetComponent<Animator>().SetBool("playAttacking", false);

            attacking = false;
            //wasAttacking = false;
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, startingLightHitBox, attackingGrowSpeed * 2 * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, attackingGrowSpeed * 2 * Time.deltaTime);
        }
        return attacking;
    }

    public bool candleDrop()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && !busy && !attacking)
        {
            if(coroutineRunning)
            {
                StopCoroutine(dropFlameWaxCoroutine);
            }
            candleDropping = true;
            UnityEngine.Debug.Log("candleDropping: " + candleDropping);
            //play animation
            //create new candle object
            if (!dropFlameLight.enabled || !dropFlameSprite.enabled)
            {
                dropFlameLight.enabled = true;
                dropFlameSprite.enabled = true;
            }
            dropFlame.transform.position = gameObject.transform.position;
            dropFlameWax = 20.0f;
            StartCoroutine(dropFlameWaxCoroutine);

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
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox*0, attackingGrowSpeed * 3 * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, 0, attackingGrowSpeed * 3 * Time.deltaTime);
        }
    }
    // addRate: rate at which wax meter increases
    public void addWax(float addRate = 10.0f)
    {
        waxCurrent += (addRate) * Time.deltaTime;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // If player touches a hazard
        if (other.CompareTag("Hazard")) {
            waxCurrent -= 20.0f;
            if(waxCurrent <= 0)
            {

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
    
    }

    IEnumerator dropFlameCoroutine()
    {
        coroutineRunning = true;
        while(true)
        {
            yield return new WaitForSeconds(1);
            dropFlameWax -= 1.0f;
            if(dropFlameWax <= 0)
            {
                dropFlameLight.enabled = false;
                coroutineRunning = false;
                StopCoroutine(dropFlameWaxCoroutine);
            }
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
