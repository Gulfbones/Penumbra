using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    private float turnAmount = 5.0f;
    public bool handleCollected;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;
    public GameObject rails;
    // Start is called before the first frame update
    void Start()
    {
        turnAmount = 0.0f;
        //fullTurnAmount = 100.0f;
        handleCollected = false;
        rails = GameObject.Find("Tilemap_Moving_Rails");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {}

    // Collects the handle, changing the sprite and bool
    public void CollectHandle()
    {
        handleCollected = true;
        // Changes Sprite to add handle
        spriteRenderer.sprite = newSprite;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // While the player is within the radius of the crank
        if (handleCollected && other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            // Moves the rails into place while all use conditions met
            rails.transform.localPosition = Vector3.MoveTowards(rails.transform.localPosition, new Vector3(6,0,0), 1.0f * Time.deltaTime); ;
        }
    }

}
