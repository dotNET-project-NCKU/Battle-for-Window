using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
public class LocalCameraHandler : MonoBehaviour
{
    public Transform CameraAnchorPoint;
    Vector2 viewInput;

    Camera localCamera;
    public float cameraRotationX = 0;
    public float cameraRotationY = 0;
    [Networked]
    public bool isuse { get; set; }

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        isuse = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (localCamera.GetComponent<LocalCameraHandler>().isuse)
        {
            localCamera.transform.parent = null;
            Debug.Log("owo");
        }
            
        else
            localCamera.GetComponent<LocalCameraHandler>().isuse = true;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void LateUpdate()
    {
        if (CameraAnchorPoint == null)
            return;
        if (!localCamera.GetComponent<LocalCameraHandler>().isuse)
            return;
        localCamera.transform.position = CameraAnchorPoint.position;
        localCamera.transform.rotation = CameraAnchorPoint.rotation;
        if (Time.timeScale != 0)
            transform.Rotate(-3*Input.GetAxis("Mouse Y"), 0, 0);
        /*cameraRotationX += viewInput.y * Time.deltaTime * 100;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);
        cameraRotationY += viewInput.x * Time.deltaTime * 100;
        localCamera.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);*/
    }
    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
