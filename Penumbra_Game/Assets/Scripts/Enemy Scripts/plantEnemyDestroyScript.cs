using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantEnemyDestroyScript : MonoBehaviour
{
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;   
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.Log("plant health: " + health);
        //UnityEngine.Debug.Log("plant active: " + gameObject.activeInHierarchy);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Drop Flame") || other.gameObject.CompareTag("Player"))
        {
            health -= 0.75f;
            if (health <= 0)
            {
                gameObject.SetActive(false);

            }
        }
    }
}
