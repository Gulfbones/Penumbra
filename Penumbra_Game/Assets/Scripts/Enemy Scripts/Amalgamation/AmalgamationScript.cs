using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamationScript : MonoBehaviour
{
    public BoxCollider2D hitbox;
    public GameObject invulnerableLight;
    public GameObject worldLight1;
    public GameObject worldLight2;
    public GameObject ratSpawner1;
    public GameObject ratSpawner2;
    public GameObject bugEnemy1;
    public GameObject bugEnemy2;
    public GameObject stalker1;
    public GameObject stalker2;
    public GameObject stalker3;
    public GameObject stalker4;


    public int bossHealth;
    public int damage;
    private float damageInterval = 1.5f;
    public GameObject blocker;

    public float stage2Interval;
    public bool stage2Finished = false;

    public float stage3Inverval;
    public bool stage3Finished = false;

    public float stageFInterval;
    public bool stageFFinished = false;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damageInterval > 0)
        {
            damageInterval -= Time.deltaTime;
        }

        if (bossHealth <= 750 && !stage2Finished)
        {
            anim.SetTrigger("stageTwo");
            hitbox.enabled = false;
            invulnerableLight.SetActive(true);
            ratSpawner1.SetActive(true);
            ratSpawner2.SetActive(true);

            if(!stage2Finished)
            {
                stage2Interval -= Time.deltaTime;
            }

            if(!stage2Finished && stage2Interval <= 0)
            {
                anim.SetTrigger("idle");
                hitbox.enabled = true;
                invulnerableLight.SetActive(false);
                //ratSpawner1.SetActive(false);
                //ratSpawner2.SetActive(false);
                stage2Finished = true;
            }

        }
        if (bossHealth <= 500 && !stage3Finished)
        {
            ratSpawner1.SetActive(false);
            ratSpawner2.SetActive(false);

            anim.SetTrigger("stageThree");
            hitbox.enabled = false;
            invulnerableLight.SetActive(true);
            stalker1.SetActive(true);
            stalker2.SetActive(true);

            if(!stage3Finished)
            {
                stage3Inverval -= Time.deltaTime;
            }

            if(!stage3Finished && stage3Inverval <= 0)
            {
                hitbox.enabled = true;
                invulnerableLight.SetActive(false);
                stage3Finished = true;
            }

        }
        if (bossHealth <= 250)
        {
            stalker1.SetActive(false);
            stalker2.SetActive(false);

            anim.SetTrigger("stageFinal");
            worldLight1.SetActive(false);
            worldLight2.SetActive(true);
            stalker1.SetActive(true);
            stalker2.SetActive(true);
            stalker3.SetActive(true);
            stalker4.SetActive(true);
        }
        if (bossHealth <= 0)
        {
            stalker1.SetActive(false);
            stalker2.SetActive(false);
            stalker3.SetActive(false);
            stalker4.SetActive(false);
            gameObject.SetActive(false);
            blocker.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Light")
        {
            if (damageInterval <= 0)
            {
                bossHealth -= damage;
                Debug.Log("Damage Taken");
                damageInterval = 1.5f;
            }
        }
    }
}