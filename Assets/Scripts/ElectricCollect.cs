using UI;
using UnityEngine;

public class ElectricCollect : MonoBehaviour
{
    public static ElectricCollect instance;
    public static int charge = 0; 
    
    public AudioClip collectSound;
    public float volume = 1.5f;

    private bool collected;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            // A player may have multiple colliders entering in the same physics step.
            collected = true;
            charge = Mathf.Clamp(charge + 1, 0, 4);
            gameObject.SetActive(false);
            if (UIManager.instance != null) UIManager.instance.UpdateThunderCount();
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
    }

    public int GetCharge()
    {
        return charge;
    }
}
