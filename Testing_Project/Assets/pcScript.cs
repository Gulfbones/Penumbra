using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;

public class pcScript : MonoBehaviour
{
    float waxMax = 4000.0f;
    float waxCurrent;
    float standardWaxLost;
    float attackingWaxLost;
    bool attacking;
    bool busy;
    bool canInteractFountain;
    bool canInteractLantern;

    // Start is called before the first frame update
    void Start()
    {
        //waxMax = 20000.0f;
        waxCurrent = waxMax;
        standardWaxLost = 15.5f;//.5
        attackingWaxLost = 50.25f;//5.25
        canInteractFountain = false;
        canInteractLantern = false;

        attacking = false;
        busy = false;

    }

    // Update is called once per frame
    void Update()
    {
        isAttacking();
        waxMeter();
        //UnityEngine.Debug.Log(waxCurrent);//testing
        //UnityEngine.Debug.Log("isAttacking: " + isAttacking());//testing
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
        if (attacking)
        {
            waxCurrent -= attackingWaxLost * Time.deltaTime;
        }
        else
        {
            waxCurrent -= standardWaxLost * Time.deltaTime;
        }
        //makes sure current player wax doesn't go over maximum wax amount
        if (waxCurrent > waxMax)
        {
            waxCurrent = waxMax;
        }
        if (waxCurrent <= 0)
        {
            UnityEngine.Debug.Log("Game Over");
            //Destroy(gameObject); //destroy pc game object
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //UnityEngine.Debug.Log(other.tag);
        if (other.CompareTag("Hazard")) {
            waxCurrent -= 50.0f;
        }
        /*
        //UnityEngine.Debug.Log("on trigger stay attacking:" + attacking); //testing
        if (busy)
        {
            canInteractFountain = false;
            canInteractLantern = false;
        }
        else if (attacking)
        {
            canInteractFountain = false;
            canInteractLantern = false;
        }
        else if (other.gameObject.CompareTag("fountainInteract"))
        {
            canInteractFountain = true;
        }
        else if (other.gameObject.CompareTag("lanternInteract"))
        {
            canInteractLantern = true;
        }
        else
        {
            canInteractFountain = false;
            canInteractLantern = false;
        }
        */
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            waxCurrent -= 1000.0f;
        }
        /*
        if (other.gameObject.CompareTag("fountainInteract"))
        {
            canInteractFountain = false;
        }
        if (other.gameObject.CompareTag("lanternInteract"))
        {
            canInteractLantern = false;
        }
        */
    
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
    public bool getCanInteractFountain()
    {
        return canInteractFountain;
    }
    public void setCanInteractFountain(bool input)
    {
        canInteractFountain = input;
    }
    public bool getCanInteractLantern()
    {
        return canInteractLantern;
    }
    public void setCanInteractLantern(bool input)
    {
        canInteractLantern = input;
    }


}
