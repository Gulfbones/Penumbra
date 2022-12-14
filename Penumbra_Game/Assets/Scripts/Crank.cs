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
    public GameObject textNeed;
    public GameObject textCrank;
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
        textNeed = gameObject.transform.GetChild(0).gameObject;
        textCrank = gameObject.transform.GetChild(1).gameObject;

        textCrank.SetActive(false);
        textNeed.SetActive(true);
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
        textCrank.SetActive(true);
        textNeed.SetActive(false);

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (handleCollected)
            {
                if(textCrank.GetComponent<Fading>().fadeChange != 1.5f)
                    textCrank.GetComponent<Fading>().fadeIn(1.5f);
                //textCrank.SetActive(true);
                //textNeed.SetActive(false);
            }
            else
            {
                if (textNeed.GetComponent<Fading>().fadeChange != 1.5f)
                    textNeed.GetComponent<Fading>().fadeIn(1.5f);
                //textCrank.SetActive(false);
                //textNeed.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (handleCollected)
            {
                if (textCrank.GetComponent<Fading>().fadeChange != -0.8f)
                    textCrank.GetComponent<Fading>().fadeOut(-0.8f);
                //textCrank.SetActive(false);
                //textNeed.SetActive(false);
            }
            else
            {
                if (textNeed.GetComponent<Fading>().fadeChange != -0.8f)
                    textNeed.GetComponent<Fading>().fadeOut(-0.8f);
                //textNeed.SetActive(false);
                //textCrank.SetActive(false);
            }
        }
    }

}
