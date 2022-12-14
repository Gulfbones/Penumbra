using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public Slider healthSlider;
    [SerializeField] AudioClip defeatedBoss;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthSlider.value = bossHealth;
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
            anim.ResetTrigger("stageTwo");
            anim.SetTrigger("stageThree");
            head2.SetActive(false);
            head3.SetActive(true);
        }

        if(bossHealth <= 250)
        {
            anim.ResetTrigger("stageThree");
            anim.SetTrigger("stageF");
            head3.SetActive(false);
            head4.SetActive(true);
        }
        if(bossHealth <= 0)
        {
            gameObject.SetActive(false);
            blocker.SetActive(false);
            GameObject.Find("Audio Source").gameObject.GetComponent<AudioSource>().Stop();
            GameObject.Find("Audio Source").gameObject.GetComponent<AudioSource>().PlayOneShot(defeatedBoss);
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
