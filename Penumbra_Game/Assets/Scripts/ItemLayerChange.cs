using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLayerChange : MonoBehaviour
{
    public SpriteRenderer currentSprite;
    public GameObject player;
    GameObject currentObject = null;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSprite = gameObject.GetComponent<SpriteRenderer>();

        if (player.transform.position.y > currentSprite.transform.position.y)
        {
            currentSprite.sortingOrder = 12;
            Debug.Log("Changed layer to 12");
        }
        if (player.transform.position.y < currentSprite.transform.position.y)
        {
            currentSprite.sortingOrder = 1;
            Debug.Log("Changed layer to 1");
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

