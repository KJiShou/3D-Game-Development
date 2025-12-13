using UnityEngine;

public class CauldronSound : MonoBehaviour
{
    public AudioSource bubbleSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!bubbleSource.isPlaying)
                bubbleSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bubbleSource.Stop();
        }
    }
}
