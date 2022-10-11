using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEngine;

public class candleUIScript : MonoBehaviour
{

    //private GameObject candleUI;
    public pcScript playerScript;



    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player Main").GetComponent<pcScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //6.5 //1.75
        //playerScript.getWaxCurrent / playerScript.getWaxMax;
        if (playerScript.getWaxCurrent() <= 0)
        {
            Destroy(gameObject);
            //UnityEngine.Debug.Log("UI destroyed");//testing
        }
    }
}


