using System.Collections;
using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public GameObject colliders;
    public Transform targetPos;
    private float speed = 3f;

    public GameObject halfTransparentStoneObj;
    private HalfTransparentStone halfTransparentStone;

    public MovePlatformPressurePlate[] childMovingPlatformScripts;

    public AudioSource platformAudio;
    public float fadeDuration = 1f;
    public float baseVolume = 1f;

    public bool playerEnter = false;

    private void Start()
    {
        halfTransparentStone = halfTransparentStoneObj.GetComponent<HalfTransparentStone>();
        platformAudio.loop = true;
        platformAudio.volume = 0f;
        platformAudio.Play();
        StartCoroutine(FadeInOutLoop());
    }

    private void LateUpdate()
    {

        if (halfTransparentStone.goUp)
        {
            DisableMovingPlatformScrip();
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.MoveTowards(pos.y, 10.0f, speed * Time.deltaTime);
            pos.y = Mathf.Min(pos.y, 10.0f);
            transform.localPosition = pos;
        }
        else if (halfTransparentStone.goDown)
        {
            DisableMovingPlatformScrip();
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.MoveTowards(pos.y, -4.2f, speed * Time.deltaTime);
            pos.y = Mathf.Max(pos.y, -4.2f);
            transform.localPosition = pos;
        }
    }


    public void EnableMovingPlatformScrip()
    {
        colliders.SetActive(true);
        foreach (MovePlatformPressurePlate mp in childMovingPlatformScripts)
        {
            mp.enabled = true;
        }
    }

    public void DisableMovingPlatformScrip()
    {
        colliders.SetActive(true);
        foreach (MovePlatformPressurePlate mp in childMovingPlatformScripts)
        {
            mp.enabled = false;
        }
    }

    public void InactiveColliders()
    {
        colliders.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnter = false;
        }
    }

    private bool IsMoving()
    {
        return halfTransparentStone.goUp || halfTransparentStone.goDown;
    }

    private IEnumerator FadeTo(float targetVolume, float duration)
    {
        float startVolume = platformAudio.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            platformAudio.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        platformAudio.volume = targetVolume;
    }

    private IEnumerator FadeInOutLoop()
    {
        while (true)
        {
            float targetVolume = IsMoving() ? baseVolume : 0f;
            // fade in
            yield return StartCoroutine(FadeTo(targetVolume, fadeDuration));

            targetVolume = IsMoving() ? baseVolume : 0f;
            // fade out
            yield return StartCoroutine(FadeTo(0f, fadeDuration));
        }
    }
}
