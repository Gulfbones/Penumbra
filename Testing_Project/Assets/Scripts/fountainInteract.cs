using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainInteract : MonoBehaviour
{
    bool used;
    public pcScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
        playerScript = GameObject.FindGameObjectWithTag("pc").GetComponent<pcScript>();
    }

    // Update is called once per frame
    void Update()
    {
        useFunction(playerScript.getCanInteractFountain());
        if (playerScript.getWaxCurrent() <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool useFunction(bool canInteract)
    { 
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !used)
        {
            used = true;
            playerScript.setWaxCurrent(playerScript.getWaxCurrent() + 7500.0f); ;
        }
        return used;
    }

}
