using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currencyScript : MonoBehaviour
{
    float currency;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currency = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
