using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera orthoCam;

    void Start()
    {
        firstPersonCam.enabled = true;
        orthoCam.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            firstPersonCam.enabled = !firstPersonCam.enabled;
            orthoCam.enabled = !orthoCam.enabled;
        }
    }
}
