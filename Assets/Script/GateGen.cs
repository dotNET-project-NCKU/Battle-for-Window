using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GateGen : MonoBehaviour
{
    //public GameObject[] myObjArray;

    // Use this for initialization
    void Start()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "bars");
        foreach (GameObject fooObj in objects)
        {
            if (fooObj.name == "bars")
            {
                //Do Something
                Debug.Log(fooObj.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
