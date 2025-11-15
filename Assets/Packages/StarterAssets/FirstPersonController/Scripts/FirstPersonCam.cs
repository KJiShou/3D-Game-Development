using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }
}
