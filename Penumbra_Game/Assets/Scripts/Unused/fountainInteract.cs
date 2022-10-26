using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainInteract : MonoBehaviour
{
    bool lit;
    public PlayerScript playerScript;
    public GameObject lightGameObject;
    GameObject currentObject = null;
    //public Rigidbody2D activeRadius;

    // Start is called before the first frame update
    void Start()
    {
        //activeRadius = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lightGameObject = transform.GetChild(0).gameObject;
        //lanternLight = lightGameObject.GetComponent<Light2D>();
        //lanternLight.enabled = false;
        lightGameObject.SetActive(false); // default disables light
    }

    // Update is called once per frame
    void Update()
    {
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
    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("In radius");
        //Debug.Log(other.tag);
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E)) // While the player is within the radius of the lantern
        {
            Debug.Log("Using Fountain");
            //lit = true;
            playerScript.addWax();
        }
    }
    */
}
