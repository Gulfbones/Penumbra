using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    public float fadeAmount,fadeChange;
    public SpriteRenderer spriteRenderer;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        fadeAmount = 1.0f;
        fadeChange = -0.5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r,color.g,color.b,fadeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if(!(fadeAmount > 1 || fadeAmount < 0))
        {
            fadeAmount += fadeChange * Time.deltaTime;
        }
        spriteRenderer.color = new Color(color.r,color.g,color.b,fadeAmount);
    }
    /*
     * Solid to transparent
     */
    public void fadeOut(float change = -0.8f,bool reset = true) 
    {
        fadeChange = change;
        if(reset) fadeAmount = 1.0f;
    }
    /*
     * Transparent to Solid
     */
    public void fadeIn(float change = 0.8f, bool reset = true) 
    {
        fadeChange = change;
        if(reset) fadeAmount = 0.0f;
    }
}
