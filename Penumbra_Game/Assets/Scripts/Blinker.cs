using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    private GameObject eyes;
    private Vector3 posBeforeBlink;
    public float blinkTime;
    public float blinkSpeed = 20.0f; // How fast eyes scale (arbitrary speed value)

    // Start is called before the first frame update
    void Start()
    {
        blinkTime = 5.0f;
        eyes = gameObject;
        posBeforeBlink = eyes.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // counts down
        blinkTime -= Time.deltaTime;
        if(blinkTime <= 0.35f && blinkTime >= 0.25f) // Close eyes
        {
            // Scales eyes Down
            eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, new Vector3(1,0,1), blinkSpeed * Time.deltaTime);
        }
        else
        {
            // Scales eyes Up
            //eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, new Vector3(1, 1, 1), blinkSpeed * Time.deltaTime);
            eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, posBeforeBlink, blinkSpeed * Time.deltaTime);
        }
        if(blinkTime <= 0.0f)
        {
            // Resets blink time somewhere between 2 - 10 seconds
            blinkTime = Random.Range(2.0f, 10.0f);
            posBeforeBlink = eyes.transform.localScale;
        }

        
    }
}
