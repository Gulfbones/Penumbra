using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
//using System.Security.Policy;
using UnityEngine;

public class candleUIBottomScript : MonoBehaviour
{

    public pcScript playerScript;

    private float currScaleY;
    private Vector3 newScale;
    private Vector3 origScale;
    private float maxScaleX;
    private float maxScaleY;
    private float maxScaleZ;

    private float scaleLostNum;
    private float posY;
    private float posX;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("pc").GetComponent<pcScript>();
        maxScaleX = gameObject.transform.localScale.x;
        maxScaleY = gameObject.transform.localScale.y;
        maxScaleZ = gameObject.transform.localScale.z;
        currScaleY = maxScaleY;
        newScale = new Vector3(maxScaleX, currScaleY, maxScaleZ);
        origScale = new Vector3(maxScaleX, maxScaleY, maxScaleZ);
        //UnityEngine.Debug.Log("starting scale: " + gameObject.transform.localScale);//testing
        gameObject.transform.localScale = origScale;
        //UnityEngine.Debug.Log("starting second scale: " + gameObject.transform.localScale);//testing

        scaleLostNum = 1f;
        posY = gameObject.transform.localPosition.y;
        posX = gameObject.transform.localPosition.x;

    }

    // Update is called once per frame
    void Update()
    {
        currScaleY = (playerScript.getWaxCurrent() * maxScaleY) / playerScript.getWaxMax();
        scaleLostNum = currScaleY / maxScaleY;
        newScale.y = currScaleY;
        gameObject.transform.localScale = newScale;
        //UnityEngine.Debug.Log("new scale: " + gameObject.transform.localScale);//testing


    }
    public float getCurrScaleY()
    {
        return currScaleY;
    }
    public float getMaxScaleY()
    {
        return maxScaleY;
    }
    public float getPosY()
    {
        return posY;
    }
    public float getPosX()
    {
        return posX;
    }
    public float getScaleLostNum()
    {
        return scaleLostNum;
    }
}
