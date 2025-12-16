using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    public AudioClip inPortalSound;
    public AudioClip outPortalSound; 
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
            InPortalSound();
            controller.enabled = false;
            other.transform.position = transportPosition.position;
            OutPortalSound();
            controller.enabled = true;
        }
    }

    private void InPortalSound()
    {
        if (inPortalSound != null)
        {
            AudioSource.PlayClipAtPoint(inPortalSound, transportPosition.position, 1.5f);
        }
    }

    private void OutPortalSound()
    {
        if (outPortalSound != null)
        {
            AudioSource.PlayClipAtPoint(outPortalSound, transportPosition.position, 1.5f);
        }
    }
}
