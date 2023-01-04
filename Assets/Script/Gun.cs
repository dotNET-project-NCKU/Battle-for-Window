using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform attackpoint;
    public GameObject bulletprefab;
    public float bulletspeed = 10;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletprefab, attackpoint.position, attackpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = attackpoint.forward * bulletspeed;
        }
    }
}
