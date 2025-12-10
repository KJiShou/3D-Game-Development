using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    public Transform transportPosition;
    public Transform playerPosition;
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

            Debug.Log("Teleport successful via CharacterController method.");
        }
        else
        {
            other.transform.position = transportPosition.position;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                rb.position = transportPosition.position;
                Debug.LogWarning("Object has a non-kinematic Rigidbody. Using Rigidbody.position for better stability.");
            }
        }

    }
}
