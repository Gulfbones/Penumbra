using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankHandle : MonoBehaviour
{
    public Crank cranker; // Script for crank
    public GameObject textCollected;
    public bool fade;
    public SpriteRenderer colorText;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        cranker = GameObject.Find("Crank").GetComponent<Crank>();
        textCollected = gameObject.transform.GetChild(0).gameObject;
        fade = false;
        colorText = textCollected.GetComponent<SpriteRenderer>();
        fadeTime = 1;
    }

    // Update is called once per frame :)
    void Update() 
    {
        if (fade)
        {
            Debug.Log("fading");
            //textCollected.GetComponent<SpriteRenderer>().color = new Color(colorText.r, colorText.g, colorText.b, 1 - (1 * Time.deltaTime));
            fadeTime -= 0.75f * Time.deltaTime;
            colorText.color = new Color(colorText.color.r, colorText.color.g, colorText.color.b, fadeTime);
            //candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, 0, attackingGrowSpeed * 3 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Collected Handle");
            cranker.CollectHandle();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            textCollected.SetActive(true);
            fade = true;
            //Destroy(gameObject);
        }
    }
}
