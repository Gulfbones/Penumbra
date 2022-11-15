using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Puzzle_Lantern : MonoBehaviour
{
    // Start is called before the first frame update
    public bool lit;
    private PlayerScript playerScript;
    private Puzzle_Lantern targetLantern;
    private Light2D lanternLight;
    private GameObject lightGameObject;
    GameObject currentObject = null;
    private Collider2D ownCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        ownCollider = gameObject.GetComponent<Collider2D>();
        lightGameObject = transform.GetChild(0).gameObject;
        lightGameObject.SetActive(false); // default disables light
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

        if (currentObject && Input.GetKeyDown(KeyCode.E) && !playerScript.getAttacking() && !playerScript.getBusy())
        {
            //currentObject.SetActive(false);
            //playerScript.setWaxCurrent(playerScript.getWaxMax());
            FlipStatus();

            //cast a ray
            Vector2 rayOrigin = new Vector2(this.transform.position.x, this.transform.position.y);
            Debug.DrawRay(rayOrigin, new Vector3(999, 0, 0), Color.green, 5);
            //Debug.DrawRay(rayOrigin, new Vector3(-999, 0, 0), Color.green, 5);
            //Debug.DrawRay(rayOrigin, new Vector3(0,999, 0), Color.green, 5);
            //Debug.DrawRay(rayOrigin, new Vector3(0,-999, 0), Color.green, 5);
            //apply layer mask

            int theLayerMask = LayerMask.GetMask("Puzzle_Light_Collider");
            ownCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, Mathf.Infinity, theLayerMask);
            rayCaster(hit);
            hit = Physics2D.Raycast(rayOrigin, Vector2.left, Mathf.Infinity, theLayerMask);
            rayCaster(hit);
            hit = Physics2D.Raycast(rayOrigin, Vector2.up, Mathf.Infinity, theLayerMask);
            rayCaster(hit);
            hit = Physics2D.Raycast(rayOrigin, Vector2.down, Mathf.Infinity, theLayerMask);
            rayCaster(hit);
            ownCollider.enabled = true;
        }
    }
    private void rayCaster(RaycastHit2D hit)
    {
        //Vector2 rayOrigin = new Vector2(this.transform.position.x, this.transform.position.y);
        


        if (hit == false)
        {
            Debug.Log("Cast a ray. Didn't hit anything");
        }
        else
        {
            Debug.Log("Hit!. Looks like we hit" + hit.transform.gameObject.name);

            //if it is a lantern
            if (hit.transform.gameObject.tag == "Lantern")
            {
                targetLantern = hit.transform.gameObject.GetComponent<Puzzle_Lantern>();
                targetLantern.FlipStatus();
            }
        }
    }
    private void FlipStatus()
    {
        if (lit)
        {
            lit = false;
            lightGameObject.SetActive(false);
        }
        else
        {
            lit = true;
            lightGameObject.SetActive(true);
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
