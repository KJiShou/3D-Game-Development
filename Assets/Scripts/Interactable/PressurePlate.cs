using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PressurePlate : MonoBehaviour
{
    [Header("Activate Object")]
    [Tooltip("Need to activate the object or not")]
    public GameObject activeObject;
    public bool triggeredActivate = false;
    public bool triggeredStatus = true;

    [Header("Activate Object animator")]
    [Tooltip("Need to activate the object or not")]
    public Animator activeObjectAnim;
    public bool triggeredAnimation = false;
    public string animatorTrigger = "";

    [Header("Activate Object audio source")]
    [Tooltip("Need to play the object audio clip or not")]
    public AudioSource activeObjectAudio;
    public float fadeDuration = 3f;

    public bool shake = false;
    public bool isPressed;
    private Animator animator;

    public AudioClip audioClip;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (animatorTrigger.Length <= 0)
        {
            animatorTrigger = "isPressed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed && triggeredActivate) activeObject.SetActive(triggeredStatus);
        if (!isPressed && triggeredActivate) activeObject.SetActive(!triggeredStatus);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone" && gameObject.tag == "Interactable")
        {
            audioSource.PlayOneShot(audioClip);
            isPressed = true;
            if (shake) CameraShake.Instance.ShakeCamera(2, 3.0f, 10.0f);
            animator.SetBool("isPressed", true);
            activeObjectAnim.SetBool(animatorTrigger, true);
            if(activeObjectAudio != null)
            {
                activeObjectAudio.Play();
                StartCoroutine(WaitForSFX());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone" && gameObject.tag == "Interactable")
        {
            audioSource.PlayOneShot(audioClip);
            isPressed = false;
            animator.SetBool("isPressed", false);
            activeObjectAnim.SetBool(animatorTrigger, false);
            if (shake) CameraShake.Instance.ShakeCamera(2, 3.0f, 10.0f);
            if (activeObjectAudio != null)
            {
                activeObjectAudio.Play();
                StartCoroutine(WaitForSFX());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(audioClip);
            isPressed = true;
            animator.SetBool("isPressed", true);
            activeObjectAnim.SetBool(animatorTrigger, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(audioClip);
            isPressed = false;
            animator.SetBool("isPressed", false);
            activeObjectAnim.SetBool(animatorTrigger, false);
        }
    }

    IEnumerator WaitForSFX()
    {
        float startVolume = activeObjectAudio.volume;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            activeObjectAudio.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        activeObjectAudio.Stop();
        activeObjectAudio.volume = startVolume;
    }
}
