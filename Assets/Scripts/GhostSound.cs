using UnityEngine;

public class GhostSound : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player;
    public float maxDistance = 20f;
    public float maxVolume = 1.2f; 


    void Start()
    {
        audioSource.volume = 0f;
        audioSource.Play();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        float t = 1 - Mathf.Clamp01(distance / maxDistance);
        audioSource.volume = t * maxVolume;

    }
}
