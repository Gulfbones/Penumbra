using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainInteract : MonoBehaviour
{
    //bool lit;
    //bool used;
    //float waxLeft;

    public PlayerScript playerScript;
    public GameObject lightGameObject;
    GameObject currentObject = null;
    //public Rigidbody2D activeRadius;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lightGameObject = transform.GetChild(0).gameObject;
        
        lightGameObject.SetActive(false); // default disables light

        //used = false;
        //waxLeft = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.getWaxCurrent() <= 0)
        {
            lightGameObject.SetActive(false);
        }

        if (currentObject && Input.GetKey(KeyCode.E))
        {
            lightGameObject.SetActive(true);
            playerScript.addWax();
        }

    }

    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            currentObject = other.gameObject;

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject == currentObject)
            {
                currentObject = null;
            }
        }


    }
    
}
