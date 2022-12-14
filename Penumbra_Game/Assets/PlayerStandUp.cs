using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static EnemyScript_02;

public class PlayerStandUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation theAnimation;
    public Animator theAnimator;
    public Animator theAnimator2;
    [SerializeField] Light2D candleLight;
    public bool started,close;
    public float originalLightSize;
    void Start()
    {
        gameObject.GetComponent<PlayerScript>().enabled = false;
        gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        theAnimator = GameObject.Find("Player_Character_right").gameObject.GetComponent<Animator>();
        //theAnimator2 = GameObject.Find("Player_Character_right").gameObject.GetComponent<Animator>().Play(""layer)//GetLayerIndex("Bottom");
        theAnimator.Play("side sit");
        theAnimator.Play("sits",1);
        started = false;
        close =false;
        originalLightSize = candleLight.pointLightOuterRadius;
        candleLight.pointLightOuterRadius = originalLightSize * 0.25f;//Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize * 0.5f, 15.0f * Time.deltaTime);
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
        if (started)
        {
            if (!close)
            {
                candleLight.pointLightOuterRadius = Mathf.MoveTowards(candleLight.pointLightOuterRadius, originalLightSize, 15.0f * Time.deltaTime);
                if (originalLightSize - candleLight.pointLightOuterRadius < 1.0f)
                close = true;
            }
            
        }
    }

    public IEnumerator StandCoroutine()
    {
        
        //playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.75f);
        theAnimator.Play("Bottoms_Idle",1);
        gameObject.GetComponent<PlayerScript>().enabled = true;
        gameObject.GetComponent<PlayerMovementScript>().enabled = true;

    }
}
