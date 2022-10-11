using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinker : MonoBehaviour
{
    private GameObject eyes;
    public float blinkTime = 5.0f;
    public float blinkSpeed = 20.0f;
    private Vector3 eyesPos;
    private Vector3 eyesGoal;

    // Start is called before the first frame update
    void Start()
    {
        eyes = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        blinkTime -= Time.deltaTime;
        if(blinkTime <= 0.35f && blinkTime >= 0.25f) // Close eyes
        {
            eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, new Vector3(1,0,1), blinkSpeed * Time.deltaTime);
            //blink = true;
        }
        else
        {

            eyes.transform.localScale = Vector3.MoveTowards(eyes.transform.localScale, new Vector3(1, 1, 1), blinkSpeed * Time.deltaTime);
            
        }
        if(blinkTime <= 0.0f)
        {
            blinkTime = Random.Range(2.0f, 10.0f);
        }

        
    }
}
