using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.XR;

public class PlayerScript : MonoBehaviour
{
    float waxMax = 120.0f; // This float value represents amount of seconds the candle can burn
    float waxCurrent;
    float standardWaxLost;
    float attackingWaxLost;

    [SerializeField] GameObject lightHitBox;
    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D candleLight;
    float originalLightSize;
    //GameObject lightHitBox;
    Vector3 startingLightHitBox;
    Vector3 attackingLightHitBox;
    float attackingGrowSpeed;

    bool wasAttacking;
    bool attacking;
    bool busy;
    //bool canInteractFountain;
    //bool canInteractLantern;

    // Start is called before the first frame update
    void Start()
    {
        //lightHitBox = GameObject.Find("Light_Hitbox");
        waxCurrent = waxMax;
        standardWaxLost = 1.0f; //.5
        attackingWaxLost = 5.0f; //5.25
        startingLightHitBox = new Vector3(lightHitBox.transform.localScale.x, lightHitBox.transform.localScale.y, lightHitBox.transform.localScale.z);//new Vector(lightHitBox.transform.localPosition.x, lightHitBox.transform, lightHitBox.transform);
        attackingLightHitBox = new Vector3(lightHitBox.transform.localScale.x*2.0f, lightHitBox.transform.localScale.y*1.5f, lightHitBox.transform.localScale.z);//new Vector3(1.0f,1.0f,0.0f);
        attackingGrowSpeed = 15.0f;
        originalLightSize = candleLight.pointLightOuterRadius;

        wasAttacking = false;
        attacking = false;
        busy = false;

    }

    // Update is called once per frame
    void Update()
    {
        isAttacking();
        waxMeter();
    }


    public bool isAttacking()
    {
        //Check if pc is attacking
        if (Input.GetKey(KeyCode.UpArrow) && !busy)
        {
            if(!wasAttacking)
            {
                //play starting attack animation
            }
            else
            {
                //play middle attack animation
            }
            attacking = true;
            wasAttacking = true;
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, attackingLightHitBox, attackingGrowSpeed * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize*2, attackingGrowSpeed * Time.deltaTime);
        }
        else
        {
            if (wasAttacking)
            {
                //play ending attack animation
            }
            attacking = false;
            wasAttacking = false;
            lightHitBox.transform.localScale = Vector3.MoveTowards(lightHitBox.transform.localScale, startingLightHitBox, attackingGrowSpeed * 2 * Time.deltaTime);
            candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, attackingGrowSpeed * 2 * Time.deltaTime);
        }
        return attacking;
    }
    public void waxMeter()
    {
        //Wax meter logic
        if (!attacking)
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
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // If player touches a hazard
        if (other.CompareTag("Hazard")) {
            waxCurrent -= 20.0f;
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
