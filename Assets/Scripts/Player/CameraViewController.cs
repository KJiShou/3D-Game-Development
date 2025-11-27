using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraViewController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public StarterAssetsInputs input;

    public float zoomSpeed = 10f;
    public float sprintFOVIncrease = 15f;   // how much wider when sprinting
    public float smoothSpeed = 10f;

    public float minFOV = 20f;
    public float maxFOV = 90f;

    private float baseFOV = 50f; // will update dynamically with scroll
    private float targetFOV;

    void Start()
    {
        // Start with camera's current default FOV
        baseFOV = virtualCamera.m_Lens.FieldOfView;
        targetFOV = baseFOV;
    }

    void Update()
    {
        if (virtualCamera == null) return;

        // --------- 1. Mouse Scroll (Zoom) ----------
        float scroll = 0f;

        if (Mouse.current != null && Mouse.current.scroll != null && Mouse.current.scroll.ReadValue().y != 0)
        {
            scroll = Mouse.current.scroll.ReadValue().y;
            scroll *= 0.1f;  // New Input System scroll is large; reduce it
        }
        else
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
        }

        if (scroll != 0)
        {
            baseFOV -= scroll * zoomSpeed;
            baseFOV = Mathf.Clamp(baseFOV, minFOV, maxFOV);
        }

        // --------- 2. Sprint FOV Offset ----------
        float sprintOffset = input.sprint ? sprintFOVIncrease : 0f;

        // Target FOV
        targetFOV = Mathf.Clamp(baseFOV + sprintOffset, minFOV, maxFOV);
        // Debug.Log("target FOV: " + targetFOV);

        // --------- 3. Smooth Transition ----------
        virtualCamera.m_Lens.FieldOfView =
            Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
    }
}
