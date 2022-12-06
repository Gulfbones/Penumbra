using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public Vector3 rightMovement, leftMovement, upMovement, downMovement;
    public Quaternion desiredAngle;

    public GameObject down, up, left, right;
    public GameObject playerObject;

    public Rigidbody2D playerPhysicsEngine;

    public float animTriggerVelocity = 9.5f; // How fast you need to move to trigger walk animation
    
    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer PlayerCandleSpriteRenderer;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start ran");

        // Getting sprite Animation and rendering for player
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

        // Getting physics engine
        playerPhysicsEngine = GetComponent<Rigidbody2D>();

        // Gets the first child gameObject of the main player 
        playerObject = gameObject.transform.GetChild(0).gameObject;
         
        // Gets the the SpriteRenderer of the first childs, first child of the main player
        PlayerCandleSpriteRenderer = playerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        // Setting GameObject directions
        down = gameObject.transform.GetChild(1).gameObject;
        up = gameObject.transform.GetChild(2).gameObject;
        left = gameObject.transform.GetChild(3).gameObject;
        right = gameObject.transform.GetChild(4).gameObject;

        rightMovement = new Vector3(10, 0, 0);  // x, y, z
        leftMovement = new Vector3(-10, 0, 0);  // x, y, z
        upMovement = new Vector3(0, 10, 0);     // x, y, z
        downMovement = new Vector3(0, -10, 0);  // x, y, z

        // Sets desired Angle to current
        desiredAngle = playerObject.transform.rotation;
        StartCoroutine(rotateCoroutine());
    }
    // Clears all the player game objects
    void ClearActive(string direction)
    {
        if (!UIManager.isPaused) //isPaused is from UIManager.cs, also attached to the player
        { 
            if (direction != "up") foreach (var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
            if (direction != "down") foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
            if (direction != "left") foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
            if (direction != "right") foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.isPaused) //isPaused is from UIManager.cs, also attached to the player
        {
            // Can only move LEFT or RIGHT
            if (Input.GetKey(KeyCode.D)) // RIGHT
            {
                isMoving = true;
                foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
                ClearActive("right");
                //playerObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Sets the Rotation of the child object "Player Object" which also rotates all other children objects
                desiredAngle = Quaternion.Euler(0, 0, 0);


                playerPhysicsEngine.velocity = new Vector3(rightMovement.x, playerPhysicsEngine.velocity.y, 0); // Adds Velocity which causes movement
            }
            else if (Input.GetKey(KeyCode.A)) // LEFT
            {
                isMoving = true;
                foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
                ClearActive("left");
                //playerObject.transform.rotation = Quaternion.Euler(0, 0, 180); // LEFT
                desiredAngle = Quaternion.Euler(0, 0, 180); // LEFT


                playerPhysicsEngine.velocity = new Vector3(leftMovement.x, playerPhysicsEngine.velocity.y, 0);

            }

            // Can only move UP or DOWN
            if (Input.GetKey(KeyCode.W)) // UP
            {
                isMoving = true;
                foreach (var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
                ClearActive("up");
                //playerObject.transform.rotation = Quaternion.Euler(0, 0, 90); // UP
                desiredAngle = Quaternion.Euler(0, 0, 90); // UP

                playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, upMovement.y, 0);


            }
            else if (Input.GetKey(KeyCode.S)) // DOWN
            {
                isMoving = true;
                foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
                ClearActive("down");
                //playerObject.transform.rotation = Quaternion.Euler(0, 0, 270); // DOWN
                desiredAngle = Quaternion.Euler(0, 0, 270); // DOWN

                playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, downMovement.y, 0);
            }

            // If velocity > animTriggerSpeed, play 
            if (playerPhysicsEngine.velocity.x > animTriggerVelocity || playerPhysicsEngine.velocity.y > animTriggerVelocity
                || playerPhysicsEngine.velocity.x < -animTriggerVelocity || playerPhysicsEngine.velocity.y < -animTriggerVelocity)
            {
                // Debug.Log("playing walk");
                up.GetComponent<Animator>().SetBool("playWalk", true);
                down.GetComponent<Animator>().SetBool("playWalk", true);
                left.GetComponent<Animator>().SetBool("playWalk", true);
                right.GetComponent<Animator>().SetBool("playWalk", true);
            }
            else
            {
                isMoving = false;
                up.GetComponent<Animator>().SetBool("playWalk", false);
                down.GetComponent<Animator>().SetBool("playWalk", false);
                left.GetComponent<Animator>().SetBool("playWalk", false);
                right.GetComponent<Animator>().SetBool("playWalk", false);
            }

        }
    }
    public IEnumerator rotateCoroutine()
    {
        while (true)
        {
            //Debug.Log("yaaay. coroutine ran.");
            //yield return new WaitForSeconds(4);
            playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
            yield return null;
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "MinecartWall")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
}
