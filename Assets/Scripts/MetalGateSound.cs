using UnityEngine;

public class MetalGateSound : MonoBehaviour
{
    public AudioSource gateAudio;
    public AudioClip gateOpenClip;
    public AudioClip gateCloseClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOpenGateSound()
    {
        if (gateOpenClip)
            gateAudio.PlayOneShot(gateOpenClip);
    }

    public void PlayCloseGateSound()
    {
        if (gateCloseClip)
            gateAudio.PlayOneShot(gateCloseClip);
    }

}
