using UnityEngine;

public class PressurePlateSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pressClip;
    public AudioClip releaseClip;

    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            audioSource.PlayOneShot(pressClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPressed)
        {
            isPressed = false;
            if (releaseClip != null)
                audioSource.PlayOneShot(releaseClip);
        }
    }
}
