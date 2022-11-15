using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_Script : MonoBehaviour
{

    public SpriteRenderer sprite;
    public GameObject mainGameObject;
    //public GameObject cart;
    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite sideSprite;
    private Rigidbody2D CartPhysicsEngine;
    //private SpriteRenderer spriteRenderer;

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = 0.3f;

    private float temp_shake_intensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainGameObject = gameObject;
        sprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        //cart = mainGameObject.transform.GetChild(0).gameObject;
        CartPhysicsEngine = GetComponent<Rigidbody2D>();
        //Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //gameObject.get
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CartPhysicsEngine.velocity.x = " + CartPhysicsEngine.velocity.x);
        //Debug.Log("CartPhysicsEngine.velocity.y = " + CartPhysicsEngine.velocity.y);
        if (CartPhysicsEngine.velocity.x > 0.01f || CartPhysicsEngine.velocity.x < -0.01f)
        {
            shake_intensity = Mathf.Abs(CartPhysicsEngine.velocity.x * 0.012f);
            Shake();
        }
        if (CartPhysicsEngine.velocity.y > 0.01f || CartPhysicsEngine.velocity.y < -0.01f)
        {
            shake_intensity = Mathf.Abs(CartPhysicsEngine.velocity.y * 0.012f);
            Shake();
        }
        if (CartPhysicsEngine.velocity.x > 2.0f || CartPhysicsEngine.velocity.x < -2.0f)
        {
            //CartPhysicsEngine.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            sprite.sprite = sideSprite;
            //temp_shake_intensity = CartPhysicsEngine.velocity.x;
        }
        if (CartPhysicsEngine.velocity.y > 2.0f || CartPhysicsEngine.velocity.y < -2.0f)
        {
            gameObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (Mathf.Sin(Time.deltaTime * 1.0f) * 1.5f), 0);
            //gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(gameObject.transform.GetChild(0).gameObject.transform.position.x, gameObject.transform.GetChild(0).gameObject.transform.position.y + (Mathf.Sin(Time.deltaTime * 1.0f) * 1.5f), 0);
            sprite.sprite = upSprite;
            //Shake();//temp_shake_intensity = shake_intensity;//temp_shake_intensity = CartPhysicsEngine.velocity.y;
        }
        if (temp_shake_intensity > 0)
        {
            gameObject.transform.GetChild(0).transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            gameObject.transform.GetChild(0).transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }
    }
    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;
        Debug.Log("shaka");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Exit Rock")
        {
            Debug.LogWarning("YOU WIN!!!!");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "MineCartPusher")
        {
            CartPhysicsEngine.velocity = new Vector3(CartPhysicsEngine.velocity.y, CartPhysicsEngine.velocity.x, 0);
            //Physics2D.IgnoreCollision(GameObject.tag("Player").collider, GetComponent<Collider2D>());
        }
    }
}