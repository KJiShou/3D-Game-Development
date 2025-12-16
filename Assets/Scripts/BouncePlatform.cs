using UnityEngine;
using System.Collections;
using StarterAssets;

public class BouncePlatform : MonoBehaviour
{
    public float bounceForce = 10f;
    public float squashAmount = 0.6f;
    public float squashDuration = 0.08f;
    public float returnDuration = 0.12f;
    private Vector3 originalScale;
    public AudioClip bounceSFX;    
    private AudioSource audioSource;
    private Material material;

    void Start()
    {
        originalScale = transform.localScale;
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        material.DisableKeyword("_EMISSION");

    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        ThirdPersonController player = other.GetComponent<ThirdPersonController>();
        if (player != null)
        {
            player.Bounce(bounceForce); 
        }

        if (audioSource && bounceSFX)
        {
            audioSource.pitch = 1f + (bounceForce / 20f); 
            audioSource.PlayOneShot(bounceSFX);
            audioSource.pitch = 1f; 
        }

        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.green * 3.0f);
        StartCoroutine(SquashEffect());
    }
}



    IEnumerator SquashEffect()
    {
        Vector3 squashScale = new Vector3(originalScale.x, originalScale.y * squashAmount, originalScale.z);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / squashDuration;
            transform.localScale = Vector3.Lerp(originalScale, squashScale, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / returnDuration;
            transform.localScale = Vector3.Lerp(squashScale, originalScale, t);
            yield return null;
        }
        material.DisableKeyword("_EMISSION");
    }
}
