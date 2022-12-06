using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamationScript : MonoBehaviour
{

    public int bossHealth;
    public int damage;
    private float damageInterval = 1.5f;
    public GameObject blocker;

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

        if (bossHealth <= 500)
        {
            anim.SetTrigger("secondStage");
        }
        if (bossHealth <= 0)
        {
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