using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damgaeAmount = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Player")
        {
            damgaeAmount = 20;
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damgaeAmount);
            Debug.Log("player is being attaked");
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        
        if (collision.transform.tag == "Player")
        {
            damgaeAmount = 1;
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damgaeAmount);
            Debug.Log("player is being attaked");
        }
    }
}
