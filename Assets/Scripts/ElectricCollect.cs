using UnityEngine;

public class ElectricCollect : MonoBehaviour
{
    public AudioClip collectSound;
    public float volume = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            Destroy(gameObject);
        }
    }
}
