using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankHandle : MonoBehaviour
{
    public Crank cranker; // Script for crank

    // Start is called before the first frame update
    void Start()
    {
        cranker = GameObject.Find("Crank").GetComponent<Crank>();
    }

    // Update is called once per frame :)
    void Update() {}

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
            //Destroy(gameObject);
        }
    }
}
