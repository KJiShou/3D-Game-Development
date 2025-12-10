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
    public AudioClip bounceSFX;      // 弹跳音效
    private AudioSource audioSource; // 平台上的 AudioSource


    void Start()
    {
        originalScale = transform.localScale;
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
            audioSource.pitch = 1f + (bounceForce / 20f); // 调整比例
            audioSource.PlayOneShot(bounceSFX);
            audioSource.pitch = 1f; // 播完恢复
        }

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
    }
}
