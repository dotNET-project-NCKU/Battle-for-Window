using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerController2 : NetworkBehaviour
{
    [SerializeField]
    private NetworkCharacterControllerPrototype networkCharacterController = null;

    [SerializeField]
    private BulletController bulletPrefab;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Image hpBar = null;

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
    [SerializeField]
    float mouseSensitivity = 4f;

    public bool falldown = false;

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
            Debug.Log(transform.rotation * moveVector);
            //Debug.Log(pressed);
            
            float mouse_dx = Input.GetAxis("Mouse X");
            float mouse_dy = Input.GetAxis("Mouse Y") * -1;
            /*
            if (Input.GetMouseButton(1))
            {
                if (Mathf.Abs(mouse_dx) > 0 || Mathf.Abs(mouse_dy) > 0)
                {
                    // get the camera eular angle 
                    Vector3 currentCameraAngle = transform.rotation.eulerAngles;

                    currentCameraAngle.x = Mathf.Repeat(currentCameraAngle.x + 180f, 360f) - 180f;       // always true 0 ~ 180
                    currentCameraAngle.y += mouse_dx;                       // unity yaw Y, right is positive, left i negative
                    currentCameraAngle.x -= mouse_dy;                       // unity pitch X, up is negative, down is positive

                    Quaternion cameraRotation = Quaternion.identity;
                    cameraRotation.eulerAngles = new Vector3(currentCameraAngle.x, currentCameraAngle.y, 0);
                    //transform.rotation = cameraRotation;
                    networkCharacterController.WriteRotation(cameraRotation);

                }
            }*/


            if (!falldown)
            {
                //networkCharacterController.Move(moveSpeed * moveVector * Runner.DeltaTime);
                //Debug.Log(Quaternion.AngleAxis(mouseSensitivity * Input.GetAxis("Mouse X") * Runner.DeltaTime, Vector3.up));
                //networkCharacterController.WriteRotation(Quaternion.AngleAxis(mouseSensitivity * Input.GetAxis("Mouse X") * Runner.DeltaTime, Vector3.up));


                //networkCharacterController.TeleportToRotation(new Quaternion(0, mouseSensitivity * Input.GetAxis("Mouse X"), 0, Space.Self);
                //networkCharacterController.Transform.Rotate(0, mouseSensitivity * Input.GetAxis("Mouse X"), 0, Space.Self);
                networkCharacterController.Rotate(data.rotationInput.x);


                if (pressed.IsSet(InputButtons.FIRE))
                {
                    Runner.Spawn(
                        bulletPrefab,
                        muzzle.position + transform.TransformDirection(Vector3.forward),
                        Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)),
                        Object.InputAuthority);
                    networkAnimator.Animator.SetBool("shoot", true);
                }else if (buttons.IsSet(InputButtons.RUN))
                {
                    networkAnimator.Animator.SetBool("run", true);
                    
                    //* moveVector
                    //networkCharacterController.TeleportToPosition(runSpeed * moveVector * Runner.DeltaTime, interpolateBackwards: false);
                    //networkCharacterController.Transform.Translate(runSpeed * moveVector * Runner.DeltaTime);
                    networkCharacterController.Move(runSpeed * (transform.rotation * moveVector) * Runner.DeltaTime);
                }
                else if(buttons.IsSet(InputButtons.WALK))
                {
                    networkAnimator.Animator.SetBool("run", true);
                    //networkCharacterController.TeleportToPosition(moveSpeed * moveVector * Runner.DeltaTime, interpolateBackwards: false);
                    //networkCharacterController.Transform.Translate(moveSpeed * moveVector * Runner.DeltaTime);
                    networkCharacterController.Move(moveSpeed * (transform.rotation * moveVector) * Runner.DeltaTime);
                    
                }
                else
                {
                    networkAnimator.Animator.SetBool("run", false);
                    //networkCharacterController.TeleportToPosition(moveSpeed * moveVector * Runner.DeltaTime, interpolateBackwards: false);
                    //networkCharacterController.Transform.Translate(moveSpeed * moveVector * Runner.DeltaTime);
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
            //OnHpChanged();
        }
    }

    private void Update()
    {
        if(Object.HasInputAuthority)
            hpBar.fillAmount = (float)Hp / maxHp;
    }

    /*private void OnHpChanged()
    {
        hpBar.fillAmount = (float)Hp / maxHp;
    }*/

}
