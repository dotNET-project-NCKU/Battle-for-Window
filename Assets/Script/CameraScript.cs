using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
            transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        //Debug.Log("ok2");
    }
}
