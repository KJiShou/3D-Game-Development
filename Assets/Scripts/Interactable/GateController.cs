using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip openGateSound;
    public AudioClip closeGateSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("IsOpen", true);

            if (openGateSound != null)
                audioSource.PlayOneShot(openGateSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("IsOpen", false);

            if (closeGateSound != null)
                audioSource.PlayOneShot(closeGateSound);
        }
    }
}
