using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lanternInteract : MonoBehaviour
{
    bool lit;
    public PlayerScript playerScript;
    //public Light lanternLight;
    public Light2D lanternLight;
    public GameObject lightGameObject;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lightGameObject = transform.GetChild(0).gameObject;
        //lanternLight = lightGameObject.GetComponent<Light2D>();
        //lanternLight.enabled = false;
        lightGameObject.SetActive(false); // default disables light
        lit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //useFunction(playerScript.getCanInteractLantern());
        //UnityEngine.Debug.Log("can interact lantern: " + playerScript.getCanInteractLantern());
        if (playerScript.getWaxCurrent()<=0)
        {
            lightGameObject.SetActive(false);
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
    */

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("In radius");
        //Debug.Log(other.tag);
        if (lit == false && other.CompareTag("Player") && Input.GetKey(KeyCode.E)) // While the player is within the radius of the lantern
        {
            Debug.Log("Lit lantern");
            lit = true;
            lightGameObject.SetActive(true);
        }
    }
}
