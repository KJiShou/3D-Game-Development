using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    public Transform transportPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;

            other.transform.position = transportPosition.position;

            controller.enabled = true;
        }

    }
}
