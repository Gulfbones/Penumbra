using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    private float turnAmount;
    public bool handleCollected;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;
    public GameObject rails;
    public AudioSource clipRails;
    // Start is called before the first frame update
    void Start()
    {
        turnAmount = 0.5f;
        //fullTurnAmount = 100.0f;
        handleCollected = false;
        rails = GameObject.Find("Tilemap_Moving_Rails");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().Pause();
        clipRails.Play();
        clipRails.Pause();
    }

    // Update is called once per frame
    void Update() {
        if (!Input.GetKey(KeyCode.E))
        {
            gameObject.GetComponent<AudioSource>().Pause();
            clipRails.Pause();
        }
    }

    // Collects the handle, changing the sprite and bool
    public void CollectHandle()
    {
        handleCollected = true;
        gameObject.tag = "Interactable";
        // Changes Sprite to add handle
        spriteRenderer.sprite = newSprite;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (rails.transform.localPosition.x > 5.75f) // insures that the rails are in the right spot
        {
            rails.transform.localPosition = Vector3.MoveTowards(rails.transform.localPosition, new Vector3(6,0,0), turnAmount * Time.deltaTime);
        }
        else if (handleCollected && other.CompareTag("Player") && Input.GetKey(KeyCode.E)) // While the player is within the radius of the crank
        {
            gameObject.GetComponent<AudioSource>().UnPause();
            clipRails.UnPause();
            // Moves the rails into place while all use conditions met
            //gameObject.GetComponent<AudioSource>().Play();
            rails.transform.localPosition = Vector3.MoveTowards(rails.transform.localPosition, new Vector3(6,0,0), turnAmount * Time.deltaTime);
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    gameObject.GetComponent<AudioSource>().UnPause();
        //}
    }

}
