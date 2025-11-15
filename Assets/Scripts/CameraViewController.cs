using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraViewController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 10f;
    public float minFOV = 20f;
    public float maxFOV = 90f;
    // Update is called once per frame
    void Update()
    {
        if (virtualCamera != null)
        {
            float scroll;
            if (Mouse.current.scroll != null)
            {
                scroll = Mouse.current.scroll.ReadValue().y;
            }
            else
            {
                scroll = Input.GetAxis("Mouse ScrollWheel") * 10;
            }

            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            currentFOV -= scroll * zoomSpeed;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            virtualCamera.m_Lens.FieldOfView = currentFOV;
        }
    }
}
