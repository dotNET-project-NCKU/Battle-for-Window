using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 3;
    private void Start()
    {
        
    }
    private void Awake()
    {
        //Destroy(gameObject, life);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        Debug.Log("bullet collision");
        Destroy(gameObject);
        if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<zombieController>().TakeDamage(damageAmount);

        }
    }
}
