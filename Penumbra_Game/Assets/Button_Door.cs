using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Door : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] button button1;
    public int buttonsPressed;
    public int buttonsNeeded = 0;
    public float timer;

    bool open;
    void Start()
    {
        open = false;
        buttonsPressed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void increaseNeededButtons()
    {
        buttonsNeeded++;
    }
    void check()
    {
        if (buttonsPressed >= buttonsNeeded)
        {
            open = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            open=false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void pressedButton()
    {
        buttonsPressed++;
        check();
    }
    public void releasedButton()
    {
        buttonsPressed--;
        check();
    }
}
