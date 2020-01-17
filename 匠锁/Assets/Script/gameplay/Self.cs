using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour
{
    bool unlock = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unlock)
        {
            //Color newColor = new Color(this.GetComponent<Renderer>().material.color.r,
            //                           this.GetComponent<Renderer>().material.color.g,
            //                           this.GetComponent<Renderer>().material.color.b,
            //                           this.GetComponent<Renderer>().material.color.a - 0.1F);
            //this.GetComponent<Renderer>().material.color = newColor;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void setUnlocked(bool value)
    {
        this.unlock = value;
    }
}
