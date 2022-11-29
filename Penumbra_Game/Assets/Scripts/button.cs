using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] private Button_Door door; // the door the button is linked to // Private SerializedField generally better
    public bool state; // is it on or off
    public int objectsOnButton;
    public int buttonValue = 1; // How much "value" a button is worth (mostly only changed when you need an exit button)
    // Start is called before the first frame update
    void Start()
    {
        objectsOnButton = 0;
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
        if (state == false && objectsOnButton > 0)
        {
            state = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            door.pressedButton();
        }
        else if(objectsOnButton <= 0)
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
            objectsOnButton++;
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
            objectsOnButton--;
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
