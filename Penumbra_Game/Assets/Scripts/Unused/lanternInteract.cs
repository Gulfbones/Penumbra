using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternInteract : MonoBehaviour
{
    bool used;
    public PlayerScript playerScript;
    public Light lanternLight;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        lanternLight = GameObject.FindGameObjectWithTag("lanternLight").GetComponent<Light>();
        lanternLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        useFunction(playerScript.getCanInteractLantern());
        //UnityEngine.Debug.Log("can interact lantern: " + playerScript.getCanInteractLantern());
        if (playerScript.getWaxCurrent()<=0)
        {
            Destroy(gameObject);
        }

    }

    //
    public bool useFunction(bool canInteract)
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !used)
        {
            used = true;

            lanternLight.enabled = true;
            //replace with victor's stuff

        }
        return used;
    }

}
