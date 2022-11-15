using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBossScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public int bossHealth;
    public int damage;
    private float damageInterval = 1.5f;
    public GameObject head1;
    public GameObject head2;
    public GameObject head3;
    public GameObject head4;
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
        if(damageInterval > 0)
        {
            damageInterval -= Time.deltaTime;
        }

        if(bossHealth <= 750)
        {
            _particleSystem.Play();
            anim.SetTrigger("stageTwo");
            head1.SetActive(false);
            head2.SetActive(true);
        }

        if(bossHealth <= 500)
        {
            anim.SetTrigger("stageThree");
            head2.SetActive(false);
            head3.SetActive(true);
        }

        if(bossHealth <= 250)
        {
            anim.SetTrigger("stageF");
            head3.SetActive(false);
            head4.SetActive(true);
        }
        if(bossHealth <= 0)
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
                bossHealth -= 250;
                Debug.Log("Damage Taken");
                damageInterval = 1.5f;
            }
        }
    }
}
