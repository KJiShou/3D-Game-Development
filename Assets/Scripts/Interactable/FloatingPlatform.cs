using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public enum MoveDir
    {
        Idle,
        Left,
        Right,
        Forward,
        Backward
    }
    public MoveDir currentDir = MoveDir.Idle;
    public bool hitLeft = false;
    public bool hitRight = false;
    public bool hitFront = false;
    public bool hitBack = false;
    public float moveSpeed = 5.0f;

    public GameObject hintText;
    private StartMessage startMessage;

    public AudioSource platformAudio;
    public float fadeDuration = 1f;
    public float baseVolume = 1f;
    private Vector3 lastPosition;

    private void Start()
    {
        startMessage = hintText.GetComponent<StartMessage>();
        platformAudio.loop = true;
        platformAudio.volume = 0f;
        platformAudio.Play();
        StartCoroutine(FadeInOutLoop());
    }

    private void Update()
    {
        lastPosition = transform.position;
        if (currentDir == MoveDir.Idle)
            return;

        if(!hitLeft && !hitRight && !hitFront && !hitBack)
        {
            startMessage.StartFading();
        }

        Vector3 pos = transform.position;

        switch (currentDir)
        {
            case MoveDir.Left:
                if (hitLeft) break;
                pos.z += moveSpeed * Time.deltaTime;
                break;

            case MoveDir.Right:
                if (hitRight) break;
                pos.z -= moveSpeed * Time.deltaTime;
                break;

            case MoveDir.Forward:
                if (hitBack) break;
                pos.x += moveSpeed * Time.deltaTime;
                break;

            case MoveDir.Backward:
                if (hitFront) break;
                pos.x -= moveSpeed * Time.deltaTime;
                break;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            hintText.SetActive(true);
            switch (other.gameObject.name)
            {
                case "LeftBarrier":
                    {
                        hitLeft = true;
                        break;
                    }
                case "RightBarrier":
                    {
                        hitRight = true;
                        break;
                    }
                case "FrontBarrier":
                    {
                        hitFront = true;
                        break;
                    }
                case "BackBarrier":
                    {
                        hitBack = true;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            switch (other.gameObject.name)
            {
                case "LeftBarrier":
                    {
                        hitLeft = false;
                        break;
                    }
                case "RightBarrier":
                    {
                        hitRight = false;
                        break;
                    }
                case "FrontBarrier":
                    {
                        hitFront = false;
                        break;
                    }
                case "BackBarrier":
                    {
                        hitBack = false;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    public void DisplayHintText()
    {
        hintText.SetActive(true);
        startMessage.ResetTimer();
    }

    public void FadingHintText()
    {
        startMessage.StartFading();
    }

    private bool IsMoving()
    {
        float threshold = 0.001f;
        return Vector3.Distance(transform.position, lastPosition) > threshold;
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
