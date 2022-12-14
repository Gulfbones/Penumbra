using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyScript_02;

public class PlayerStandUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation theAnimation;
    public Animator theAnimator;
    public bool started;
    void Start()
    {
        gameObject.GetComponent<PlayerScript>().enabled = false;
        gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        theAnimator = GameObject.Find("Player_Character_right").gameObject.GetComponent<Animator>();
        theAnimator.Play("side sit");
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!started && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(StandCoroutine());
            theAnimator.SetTrigger("Stand");
            started = true;
        }
    }

    public IEnumerator StandCoroutine()
    {
        
        //playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<PlayerScript>().enabled = true;
        gameObject.GetComponent<PlayerMovementScript>().enabled = true;
    }
}
