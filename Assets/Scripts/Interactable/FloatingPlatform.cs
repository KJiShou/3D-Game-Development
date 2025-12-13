using TMPro;
using UnityEngine;
using static MovingPlatform;

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

    private void Start()
    {
        startMessage = hintText.GetComponent<StartMessage>();
    }

    private void Update()
    {
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
}
