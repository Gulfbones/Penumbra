using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;

public class candleUITopScript : MonoBehaviour
{
    public pcScript playerScript;
    public candleUIBottomScript candleBottomScript;

    private float oldPosX;
    private float oldPosY;
    private float oldPosZ;
    private Vector3 oldPos;

    private float newPosX;
    private float newPosY;
    private float newPosZ;
    private Vector3 newPos;


    private float startActualY;
    private float minY;
    private float oldY;
    private float newY;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("pc").GetComponent<pcScript>();
        candleBottomScript = GameObject.FindGameObjectWithTag("candleUIBottom").GetComponent<candleUIBottomScript>();
        /*UnityEngine.Debug.Log(candleBottomScript);//testing
        UnityEngine.Debug.Log(candleBottomScript.getCurrScaleY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getMaxScaleY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getPosY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getPosX());//testing
        UnityEngine.Debug.Log(candleBottomScript.getScaleLostNum());//testing*/


        oldPosX = gameObject.transform.localPosition.x;
        oldPosY = gameObject.transform.localPosition.y;
        oldPosZ = gameObject.transform.localPosition.z;
        oldPos = new Vector3(oldPosX, oldPosY, oldPosZ);
        //UnityEngine.Debug.Log("candle top position initial: " + oldPos);//testing
        newPosX = oldPosX;
        newPosY = oldPosY;
        newPosZ = oldPosZ;
        newPos = new Vector3(oldPosX, newPosY, newPosZ);
        //UnityEngine.Debug.Log("candle top position new: " + newPos);//testing


        startActualY = gameObject.transform.localPosition.y;
        minY = candleBottomScript.getPosY();
        oldY = startActualY - minY;
        newY = startActualY;
        position = new Vector3(oldPosX, newY, oldPosZ);
        /*UnityEngine.Debug.Log("startActualY start: " + startActualY);//testing
        UnityEngine.Debug.Log("minY start: " + minY);//testing
        UnityEngine.Debug.Log("oldY start: " + oldY);//testing
        UnityEngine.Debug.Log("newY start: " + newY);//testing
        UnityEngine.Debug.Log("candle top position start: " + position);//testing*/



    }

    // Update is called once per frame
    void Update()
    {
        minY = candleBottomScript.getPosY();
        oldY = startActualY - minY;
        newY = ((playerScript.getWaxCurrent() * oldY) / (playerScript.getWaxMax())) + minY;
        position.y = newY;
        gameObject.transform.localPosition = position;

        /*UnityEngine.Debug.Log("startActualY update: " + startActualY);//testing
        UnityEngine.Debug.Log("minY update: " + minY);//testing
        UnityEngine.Debug.Log("oldY update: " + oldY);//testing
        UnityEngine.Debug.Log("newY update: " + newY);//testing
        UnityEngine.Debug.Log("candle top position update: " + position);//testing


        UnityEngine.Debug.Log(candleBottomScript);//testing
        UnityEngine.Debug.Log(candleBottomScript.getCurrScaleY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getMaxScaleY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getPosY());//testing
        UnityEngine.Debug.Log(candleBottomScript.getPosX());//testing
        UnityEngine.Debug.Log(candleBottomScript.getScaleLostNum());//testing%*/
    }
}
