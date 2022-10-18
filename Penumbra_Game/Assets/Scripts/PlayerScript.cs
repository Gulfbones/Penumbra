using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float waxMax = 120.0f; // This float value represents amount of seconds the candle can burn
    float waxCurrent;
    float standardWaxLost;
    float attackingWaxLost;
    bool attacking;
    bool busy;
    //bool canInteractFountain;
    //bool canInteractLantern;

    // Start is called before the first frame update
    void Start()
    {
        waxCurrent = waxMax;
        standardWaxLost = 1.0f; //.5
        attackingWaxLost = 5.0f; //5.25

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
            attacking = true;
        }
        else
        {
            attacking = false;
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
            UnityEngine.Debug.Log("Game Over");
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
