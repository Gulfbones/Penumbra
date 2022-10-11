/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waxFountain : MonoBehaviour
{
    float waxFountainX; //fountain
    float waxFountainY; //fountain
    bool canInteractFountain; //fountain
    bool fountainDepleted; //fountain
    float fountainCurrentWax; //fountain
    bool fountainActivated; //fountain


    // Start is called before the first frame update
    void Start()
    {
        waxFountainX = 10.0f; //fountain
        waxFountainY = 10.0f; //fountain
        canInteractFountain = false; //fountain
        fountainDepleted = false; //fountain
        fountainCurrentWax = 7500.0f; //fountain
        fountainActivated = false; //fountain


    }

    // Update is called once per frame
    void Update()
    {
        //Wax fountain

        //find x and y distance between pc and wax fountain
        if (pcX > waxFountainX)
        {
            xDistFromFountain = pcX - waxFountainX;
        }
        else
        {
            xDistFromFountain = waxFountainX - pcX;
        }
        if (pcY > waxFountainY)
        {
            yDistFromFountain = pcY - waxFountainY;
        }
        else
        {
            yDistFromFountain = waxFountainY - pcY;
        }

        //check if pc can interact with wax fountain
        if (xDistFromFountain <= interactionDistance && yDistFromFountain <= interactionDistance)
        {
            canInteractFountain = true;
        }
        else
        {
            canInteractFountain = false;
        }

        //check if fountain has wax left
        if (fountainCurrentWax <= 0)
        {
            fountainDepleted = true;
        }
        else
        {
            fountainDepleted = false;
        }

        //activates wax fountain if pc is in range and fountain hasn't already been activated
        if (Input.GetKeyDown(keyCode.E) && canInteractFountain && !fountainActivated)
        {
            fountainActivated = true;
        }

        //transfers wax if fountain is activated, has wax left, and pc is in range
        if (fountainActivated && !fountainDepleted && canInteractFountain)
        {
            waxCurrent += 100;
            fountainCurrentWax -= 100;
        }
    }
}
*/