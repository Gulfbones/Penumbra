using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    //private GameObject eyes;
    private Vector3 posBeforeBlink;
    public float blinkTime;
    public float blinkSpeed = 20.0f; // How fast eyes scale (arbitrary speed value)

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("this gameObject:" + gameObject.name);
        blinkTime = 5.0f;
        //eyes = gameObject;
        posBeforeBlink = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // counts down
        blinkTime -= Time.deltaTime;
        if(blinkTime <= 0.35f && blinkTime >= 0.25f) // Close eyes
        {
            // Scales eyes Down
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1,0,1), blinkSpeed * Time.deltaTime);
        }
        else //if(blinkTime <= 0.25f)
        {
            // Scales eyes Up
            //eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, new Vector3(1, 1, 1), blinkSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, posBeforeBlink, blinkSpeed * Time.deltaTime);
            //Debug.Log("opening");
        }
        if(blinkTime <= 0.0f)
        {
            // Resets blink time somewhere between 2 - 10 seconds
            blinkTime = Random.Range(3.0f, 10.0f);
            posBeforeBlink = transform.localScale;
        }

        
    }
}
