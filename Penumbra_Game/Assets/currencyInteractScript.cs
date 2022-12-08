using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class currencyInteractScript : MonoBehaviour
{

    public bool lit;
    public PlayerScript playerScript;
    public SpriteRenderer currentSprite;
    [SerializeField] Sprite litSprite;
    [SerializeField] Sprite unlitSprite;
    public Light2D lanternLight;
    public GameObject lightGameObject;
    GameObject currentObject = null;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        currentSprite = gameObject.GetComponent<SpriteRenderer>();

        //activeRadius = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lightGameObject = transform.GetChild(0).gameObject;
        //lanternLight = lightGameObject.GetComponent<Light2D>();
        //lanternLight.enabled = false;
        lightGameObject.SetActive(false); // default disables light
        if (lit == true)
        {
            currentSprite.sprite = litSprite;
            lightGameObject.SetActive(true);
        }
        else
        {
            currentSprite.sprite = unlitSprite;
            lit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.getWaxCurrent() <= 0)
        {
            lightGameObject.SetActive(false);
            currentSprite.sprite = unlitSprite;
        }

        if (lit == false && currentObject && Input.GetKey(KeyCode.E) && !playerScript.getAttacking() && !playerScript.getBusy())
        {
            lit = true;
            lightGameObject.SetActive(true);
            currentSprite.sprite = litSprite;
            gameObject.tag = "Untagged";
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
