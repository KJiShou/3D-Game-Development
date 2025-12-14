using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public AudioClip openGateSound;
    public AudioClip friendHappySound;
    public AudioClip friendCrySound;
    public AudioClip jumpSound;
    public AudioSource playerAudioSource;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    public void PlayOpenGateSound()
    {
        audioSource.PlayOneShot(openGateSound);
    }

    public void PlayFriendHappySound()
    {
        playerAudioSource.PlayOneShot(friendHappySound);
    }

    public void PlayFriendCrySound()
    {
        playerAudioSource.PlayOneShot(friendCrySound);
    }

    public void PlayJumpSound()
    {
        playerAudioSource.PlayOneShot(jumpSound);
    }
}
