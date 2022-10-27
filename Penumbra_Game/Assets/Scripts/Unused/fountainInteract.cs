using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainInteract : MonoBehaviour
{
    //bool lit;
    bool used;
    float waxLeft;

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

        used = false;
        waxLeft = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentObject && Input.GetKey(KeyCode.E))
        {

            lightGameObject.SetActive(true);
            playerScript.addWax();
        }*/
        useFunction(currentObject);

    }

    public bool useFunction(GameObject current)
    {
        if (current!=null && Input.GetKeyDown(KeyCode.E) && !used && !playerScript.getAttacking())
        {
            waxLeft = 0.5f * playerScript.getWaxMax();
            used = true;
        }
        if (used && waxLeft > 0.0f)
        {
            waxLeft -= playerScript.getWaxMax()/3000.0f;
            if (current)
            {
                playerScript.setWaxCurrent(playerScript.getWaxCurrent() + playerScript.getWaxMax()/3000.0f);
            }
        }
        return used;
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
