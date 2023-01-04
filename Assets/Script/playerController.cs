using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    public Vector3 MovingDirection;

    MeshRenderer mr;
    [SerializeField] float movingSpeed = 10f;
    [SerializeField] float jumpForce = 10;


    Rigidbody rb;
    Animator animator;
    AudioSource footstep;
    bool run;
    NavMeshAgent agent;
    RaycastHit raycastHit;
    Vector2 velocity = Vector2.zero;
    bool on_ground;
    float axis = 0f;
    public GameObject EndgameScene;



    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        footstep = GetComponent<AudioSource>();
        /*if (Time.timeScale != 0)
        {
            EndgameScene.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", 0f);
        run = false;
        animator.SetBool("run", false);
        animator.SetBool("shoot", false);

        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.W))
            {

                transform.Translate(0, 0, movingSpeed);
                animator.SetFloat("speed", 1f);
                run = true;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {

                transform.Translate(0, 0, movingSpeed * 2);
                animator.SetFloat("speed", 1f);
                //animator.SetBool("run", true);
                //run = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -movingSpeed);
                animator.SetFloat("speed", 1f);
                //run = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-movingSpeed, 0, 0);
                animator.SetFloat("speed", 1f);
                //run = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(movingSpeed, 0, 0);
                animator.SetFloat("speed", 1f);
                //run = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("shoot", true);
            }
            /*if (run && !footstep.isPlaying)
            {
                footstep.Play();
            }
            if (!run && footstep.isPlaying)
            {
                footstep.Stop();
            }*/

            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("space");
                if (on_ground)
                {
                    Debug.Log("jump");
                    rb.AddForce(jumpForce * Vector3.up * 100);
                    //animator.SetBool("jump", true);
                    on_ground = false;
                }

            }
            if (Input.GetAxis("Mouse X") != axis)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X"), 0, Space.Self);
                axis = Input.GetAxis("Mouse X");
            }
            else
            {
                transform.Rotate(0, 0, 0, Space.Self);
            }

        }



    }
    private void OnCollisionStay(Collision collision)
    {
        on_ground = true;
        //animator.SetBool("jump", false);
        if (collision.transform.tag == "Ground")
        {
            on_ground = true;
            Debug.Log("ground");

        }

    }
}
