using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Axis : MonoBehaviour
{

    public GameObject x_Title;
    public GameObject y_Title;
    public GameObject z_Title;
    public GameObject redBar;
    public GameObject greenBar;
    public GameObject blueBar;
    

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(redBar, new Vector3(0.9573233f,-2.092251f,-12.1056f), Quaternion.identity);
        Instantiate(blueBar, new Vector3(-0.03957675f,-2.088251f,-10.9346f), Quaternion.identity);
        Instantiate(greenBar, new Vector3(-0.03957675f,-0.9192506f,-12.1056f), Quaternion.identity);
        Instantiate(x_Title, new Vector3(1.505423f,-2.111251f,-12.1156f), Quaternion.identity);
        Instantiate(y_Title, new Vector3(0.1524233f,0.2537494f,-12.1156f), Quaternion.identity);
        Instantiate(z_Title, new Vector3(-0.06357674f,-2.111251f,-9.772604f), Quaternion.identity);
        

    }



}
