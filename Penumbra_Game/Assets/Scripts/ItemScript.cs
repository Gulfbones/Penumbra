using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemScript : MonoBehaviour
{
    [SerializeField] GameObject waxBottle;
    GameObject currentObject = null;
   


    // Update is called once per frame

    void Update()
    {
        
        if (Input.GetKeyDown("e") && currentObject)
        {
            currentObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("waxbottle") )
        {
            currentObject = other.gameObject;
            
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("waxbottle"))
        {
            if (other.gameObject == currentObject)
            {
                currentObject = null;
            }
        }


    }
}
