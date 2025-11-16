using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerCamera;

    public bool lockCursor = true;
    public float zoomSpeed = 2f;
    public float minDistance = 2f;   
    public float maxDistance = 10f;

    private CharacterController controller;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public float distance = 5f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;
    private float yaw = 0f;  
    private float pitch = 20f;
    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;


    //    Vector3 angles = transform.eulerAngles;
    //    yaw = angles.y;
    //    pitch = angles.x;
    //}

    //void LateUpdate()
    //{

    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


    //    yaw += mouseX;


    //    pitch -= mouseY;
    //    pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);


    //    HandleZoom();

    //    Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

    //    Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
    //    playerCamera.position = transform.position + offset;


    //    playerCamera.LookAt(transform.position);
    //}

    //void HandleZoom()
    //{
    //    float scroll = Input.GetAxis("Mouse ScrollWheel");
    //    distance -= scroll * zoomSpeed;
    //    distance = Mathf.Clamp(distance, minDistance, maxDistance);
    //}

    private void Update()
    {
        //Debug.Log("New Mouse delta: " + Mouse.current.delta.ReadValue());
        //Debug.Log("New Mouse pos: " + Mouse.current.position.ReadValue());

        //Debug.Log("Old Mouse delta: " + new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        //Debug.Log("Old Mouse pos: " + new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        // Debug.Log("Scroll wheel: " + Input.GetAxis("Mouse ScrollWheel"));
    }
}
