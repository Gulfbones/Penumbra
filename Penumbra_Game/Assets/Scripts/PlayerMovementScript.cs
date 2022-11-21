using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public Vector3 rightMovement;
    public Vector3 leftMovement;
    public Vector3 upMovement;
    public Vector3 downMovement;

    public float desiredAngleVal;
    public Vector3 desiredAngleVec;
    public Quaternion desiredAngle;

    public float animTriggerVelocity = 9.5f; // How fast you need to move to trigger walk animation
    public Rigidbody2D playerPhysicsEngine;

    public GameObject down;
    public GameObject up;
    public GameObject left;
    public GameObject right;
    //public MeshRenderer currentDirection;

    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer PlayerCandleSpriteRenderer;

    public GameObject OriginalGameObject;
    public GameObject playerObject;

    bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start ran");

        // not GetComponent<GameObject>(); b/c itself is not a game object
        OriginalGameObject = gameObject;

        // Getting sprite Animation and rendering for player
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

        // Getting physics engine
        playerPhysicsEngine = GetComponent<Rigidbody2D>();

        // Gets the first child gameObject of the main player 
        playerObject = OriginalGameObject.transform.GetChild(0).gameObject;
         
        //Gets the the SpriteRenderer of the first childs, first child of the main player
        PlayerCandleSpriteRenderer = playerObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();


        down = OriginalGameObject.transform.GetChild(1).gameObject;//.GetComponent<MeshRenderer>();
        up = OriginalGameObject.transform.GetChild(2).gameObject;//.GetComponent<MeshRenderer>();
        left = OriginalGameObject.transform.GetChild(3).gameObject;//.GetComponent<MeshRenderer>();
        right = OriginalGameObject.transform.GetChild(4).gameObject;//.GetComponent<MeshRenderer>();
        //currentDirection = right;

        rightMovement = new Vector3(10, 0, 0);  // x, y, z
        leftMovement = new Vector3(-10, 0, 0);  // x, y, z
        upMovement = new Vector3(0, 10, 0);     // x, y, z
        downMovement = new Vector3(0, -10, 0);  // x, y, z

        // Probably not needed \/
        //Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("MinecartWall").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        desiredAngle = playerObject.transform.rotation;
        StartCoroutine(rotateCoroutine());
    }
    // Clears all the player game objects
    void ClearActive(string direction)
    {
        if(direction != "up")
            foreach(var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
            //up.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "down")
            foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //down.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "left")
            foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //left.GetComponentsInChildren<Renderer>().enabled = (false);
        if (direction != "right")
            foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = false;
        //right.GetComponentsInChildren<Renderer>().enabled = (false);
    }
    

    // Update is called once per frame
    void Update()
    {
        //playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
        //start coroutine
        /*
        if (coroutineStarted == false)
        {
            coroutineStarted = true;
        }
        */
        // Can only move LEFT or RIGHT
        if (Input.GetKey(KeyCode.D)) // RIGHT
        {
            
            
            //currentDirection = right;
            //right.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in right.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("right");
            //playerObject.transform.rotation = Quaternion.Euler(0, 0, 0); // Sets the Rotation of the child object "Player Object" which also rotates all other children objects
            desiredAngle = Quaternion.Euler(0, 0, 0);
            

            playerPhysicsEngine.velocity = new Vector3(rightMovement.x, playerPhysicsEngine.velocity.y, 0); // Adds Velocity which causes movement
        }
        else if (Input.GetKey(KeyCode.A)) // LEFT
        {
            
            //currentDirection = left;
            //left.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in left.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("left");
            //playerObject.transform.rotation = Quaternion.Euler(0, 0, 180); // LEFT
            desiredAngle = Quaternion.Euler(0, 0, 180); // LEFT
            

            playerPhysicsEngine.velocity = new Vector3(leftMovement.x, playerPhysicsEngine.velocity.y, 0);

        }
        
        // Can only move UP or DOWN
        if (Input.GetKey(KeyCode.W)) // UP
        {
            
            //currentDirection = up;
            //up.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in up.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("up");
            //playerObject.transform.rotation = Quaternion.Euler(0, 0, 90); // UP
            desiredAngle = Quaternion.Euler(0, 0, 90); // UP

            playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, upMovement.y, 0);


        }
        else if (Input.GetKey(KeyCode.S)) // DOWN
        {
            
            //currentDirection = down;
            //down.GetComponentInChildren<Renderer>().enabled = (true);
            foreach (var rend in down.GetComponentsInChildren<Renderer>(true)) rend.enabled = true;
            ClearActive("down");
            //playerObject.transform.rotation = Quaternion.Euler(0, 0, 270); // DOWN
            desiredAngle = Quaternion.Euler(0, 0, 270); // DOWN

            playerPhysicsEngine.velocity = new Vector3(playerPhysicsEngine.velocity.x, downMovement.y, 0);
        }
        
        // if velocity > animTriggerSpeed, play 
        if (playerPhysicsEngine.velocity.x > animTriggerVelocity || playerPhysicsEngine.velocity.y > animTriggerVelocity
            || playerPhysicsEngine.velocity.x < -animTriggerVelocity || playerPhysicsEngine.velocity.y < -animTriggerVelocity)
        {
            up.GetComponent<Animator>().SetBool("playWalk", true);
            down.GetComponent<Animator>().SetBool("playWalk", true);
            left.GetComponent<Animator>().SetBool("playWalk", true);
            right.GetComponent<Animator>().SetBool("playWalk", true);
            // Debug.Log("playing walk");
            //down.GetComponent<Animator>().SetBool("playWalk", true);
        }
        //else down.GetComponent<Animator>().SetBool("playWalk", false);
        else
        {
            up.GetComponent<Animator>().SetBool("playWalk", false);
            down.GetComponent<Animator>().SetBool("playWalk", false);
            left.GetComponent<Animator>().SetBool("playWalk", false);
            right.GetComponent<Animator>().SetBool("playWalk", false);
        }
        
        
    }
    public IEnumerator rotateCoroutine()
    {
        //Debug.Log("yaaay. coroutine ran. tick tock");
        while (true)
        {
            //Debug.Log("yaaay. coroutine ran. tick tock");
            //yield return new WaitForSeconds(4);
            //desiredAngleVal = desiredAngle.z;
            playerObject.transform.rotation = Quaternion.Lerp(playerObject.transform.rotation, desiredAngle, 10.0f * Time.deltaTime);
            yield return null;

            //Debug.Log("yaaay. timer is up");
            //SceneManager.LoadScene("Level2");
        }
        //yield return null;
        //Debug.Log("yaaay. timer is up");
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "MinecartWall")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
}
