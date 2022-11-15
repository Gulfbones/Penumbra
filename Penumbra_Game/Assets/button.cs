using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] Button_Door door; // the door the button is linked to
    public bool state; // is it on or off
    public int objectOn;
    // Start is called before the first frame update
    void Start()
    {
        objectOn = 0;
        door = door.GetComponent<Button_Door>();
        door.increaseNeededButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool GetState()
    {
        return state;
    }

    void checkObjectOn()
    {
        if (state == false && objectOn > 0)
        {
            state = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            door.pressedButton();
        }
        else if(objectOn <= 0)
        {
            state = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            door.releasedButton();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Object"))
        {
            objectOn++;
            checkObjectOn();


        }


    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            //if (other.gameObject == currentObject)
            //{
            //    currentObject = null;
            //}.
            objectOn--;
            checkObjectOn();
        }

    }
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Drop Flame"))
    //    {
    //        objectOn++;
    //        checkObjectOn();

    //    }
    //}
}
