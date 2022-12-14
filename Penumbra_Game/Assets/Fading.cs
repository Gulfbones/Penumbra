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

    public void fadeOut() // Solid to transparent
    {
        fadeChange = -0.8f;
        fadeAmount = 1.0f;

    }
    public void fadeIn() // transparent to Solid
    {
        fadeChange = 0.8f;
        fadeAmount = 0.0f;
    }
}
