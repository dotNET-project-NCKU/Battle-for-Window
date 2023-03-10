using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BulletController : NetworkBehaviour
{
    [Networked]
    private TickTimer life { get; set; }
    [SerializeField]
    private float bulletSpeed = 5f;
    public override void Spawned()
    {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
        else
        {
            transform.position += bulletSpeed * transform.forward * Runner.DeltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var zombie = other.GetComponent<zombieController>();
            zombie.TakeDamage(10);
            Runner.Despawn(Object);
        }
    }
}
