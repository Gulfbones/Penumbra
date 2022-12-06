using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lanternLevelChanger : MonoBehaviour
{
    public bool lit;
    public PlayerScript playerScript;
    //public Light lanternLight;
    public Light2D lanternLight;
    public GameObject lightGameObject;
    public GameObject levelChangeBox;
    GameObject currentObject = null;
    public GameObject player;
    public GameObject lantern;
    //public Rigidbody2D activeRadius;

    // Start is called before the first frame update
    void Start()
    {
        //activeRadius = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lightGameObject = transform.GetChild(0).gameObject;

        player = GameObject.FindGameObjectWithTag("Player");
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        //lanternLight = lightGameObject.GetComponent<Light2D>();
        //lanternLight.enabled = false;
        lightGameObject.SetActive(false); // default disables light
        levelChangeBox.SetActive(false);
        if (lit == true)
        {
            lightGameObject.SetActive(true);
        }
        else
        {
            lit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //useFunction(playerScript.getCanInteractLantern());
        //UnityEngine.Debug.Log("can interact lantern: " + playerScript.getCanInteractLantern());
        if (playerScript.getWaxCurrent() <= 0)
        {
            lightGameObject.SetActive(false);
        }

        if (lit == false && currentObject && Input.GetKey(KeyCode.E) && !playerScript.getAttacking() && !playerScript.getBusy())
        {
            //currentObject.SetActive(false);
            //playerScript.setWaxCurrent(playerScript.getWaxMax());
            lit = true;
            lightGameObject.SetActive(true);
            levelChangeBox.SetActive(true);

        }

        // check player location in relation to lantern and adjust layer accordingly
        
            if (GameObject.FindGameObjectWithTag("Player").transform.position.y > GameObject.FindGameObjectWithTag("Lantern").transform.position.y)
            {
                GameObject.FindGameObjectWithTag("Lantern").GetComponent<SpriteRenderer>().sortingOrder = 3;
                Debug.Log("Changed lantern layer to 3");
            }
            if (GameObject.FindGameObjectWithTag("Player").transform.position.y < GameObject.FindGameObjectWithTag("Lantern").transform.position.y)
            {
                GameObject.FindGameObjectWithTag("Lantern").GetComponent<SpriteRenderer>().sortingOrder = 1;
                Debug.Log("Changed lantern layer to 1");
            }
        
    }

    //
    /*
    public bool useFunction(bool canInteract)
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !lit)
        {
            lit = true;

            lanternLight.enabled = true;
            //replace with victor's stuff

        }
        return lit;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    */
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
        Debug.Log(other.tag);
        if (lit == false && other.CompareTag("Player") && Input.GetKey(KeyCode.E)) // While the player is within the radius of the lantern
        {
            Debug.Log("Lit lantern");
            lit = true;
            lightGameObject.SetActive(true);
        }
    }
    */
}
