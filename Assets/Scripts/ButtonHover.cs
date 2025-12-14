using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip hoverClip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverClip != null)
            audioSource.PlayOneShot(hoverClip);
    }
}
