using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerController2 : NetworkBehaviour
{
    [SerializeField]
    private NetworkCharacterControllerPrototype networkCharacterController = null;

    /*[SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Image hpBar = null;*/

    [SerializeField]
    private float moveSpeed = 15f;

    [SerializeField]
    private float runSpeed = 25f;

    [SerializeField]
    private float rescueDistance = 2f;

    [SerializeField]
    private int maxHp = 100;

    [SerializeField]
    bool run = false;

    public bool falldown = false;

    /*[Networked(OnChanged = nameof(OnHpChanged))]*/
    [Networked] 
    public int Hp { get; set; }
    [Networked]
    private NetworkMecanimAnimator networkAnimator { get; set; }


    [Networked]
    public NetworkButtons ButtonPrevious { get; set; }


    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            networkAnimator = GetComponentInChildren<NetworkMecanimAnimator>();
            
            Hp = maxHp;
        }
            
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            
            networkAnimator.Animator.SetBool("shoot", false);
            
            NetworkButtons buttons = data.buttons;
            var pressed = buttons.GetPressed(ButtonPrevious);
            ButtonPrevious = buttons;
            Vector3 moveVector = data.movementInput.normalized;
            //Debug.Log(pressed);
            if (!falldown)
            {
                //networkCharacterController.Move(moveSpeed * moveVector * Runner.DeltaTime);
                if (pressed.IsSet(InputButtons.FIRE))
                {
                    /*Runner.Spawn(
                        bulletPrefab,
                        transform.position + transform.TransformDirection(Vector3.forward),
                        Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)),
                        Object.InputAuthority);*/
                    networkAnimator.Animator.SetBool("shoot", true);
                }else if (buttons.IsSet(InputButtons.RUN))
                {
                    networkAnimator.Animator.SetBool("run", true);
                    networkCharacterController.Move(runSpeed * moveVector * Runner.DeltaTime);
                }
                else if(buttons.IsSet(InputButtons.WALK))
                {
                    networkAnimator.Animator.SetBool("run", true);
                    networkCharacterController.Move(moveSpeed * moveVector * Runner.DeltaTime);
                }
                else
                {
                    networkAnimator.Animator.SetBool("run", false);
                    networkCharacterController.Move(moveSpeed * moveVector * Runner.DeltaTime);
                }
                
                //Debug.Log(moveVector);
                
                
                
                
                /*if (pressed.IsSet(InputButtons.RESCUE))
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    Debug.Log(players.Length);
                    foreach (GameObject player in players)
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) < rescueDistance && player.GetComponent<PlayerController2>().falldown == true)
                        {
                            player.GetComponent<PlayerController2>().Respawn();
                        }
                    }
                    /*
                    Debug.Log(Vector3.Distance(transform.position, player.transform.position));
                    Debug.Log(player.GetComponent<PlayerController>().falldown);
                    if (Vector3.Distance(transform.position, player.transform.position) < rescueDistance && player.GetComponent<PlayerController>().falldown == true)
                    {
                        player.GetComponent<PlayerController>().Respawn();
                    }

                }*/
            }
            else
            {
                // add some falldown action.
            }

        }

        if (Hp <= 0)
        {
            falldown = true;
        }
    }

    private void Respawn()
    {
        //networkCharacterController.transform.position = Vector3.up * 2;
        Hp = maxHp;
        falldown = false;
    }

    public void TakeDamage(int damage)
    {
        if (Object.HasStateAuthority)
        {
            Hp -= damage;
        }
    }

    private void Update()
    {
        ;
    }

    /*private static void OnHpChanged(Changed<PlayerController> changed)
    {
        changed.Behaviour.hpBar.fillAmount = (float)changed.Behaviour.Hp / changed.Behaviour.maxHp;
    }*/

}
