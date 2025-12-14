using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    public AudioClip inPortalSound;
    public AudioClip outPortalSound; 
    private AudioSource audioSource;
    public Transform transportPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (audioSource != null && inPortalSound != null)
        {
            audioSource.PlayOneShot(inPortalSound);
        }
    }

    private void OutPortalSound()
    {
        if (audioSource != null && outPortalSound != null)
        {
            audioSource.PlayOneShot(outPortalSound);
        }
    }
}
