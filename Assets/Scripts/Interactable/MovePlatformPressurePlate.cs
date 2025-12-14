using System;
using UnityEngine;

public class MovePlatformPressurePlate : MonoBehaviour
{
    public GameObject platform;
    private FloatingPlatform floatingPlatform;
    public FloatingPlatform.MoveDir dir;

    private void Start()
    {
        floatingPlatform = platform.GetComponent<FloatingPlatform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!GetComponent<MovePlatformPressurePlate>().isActiveAndEnabled) return;
            if (floatingPlatform.hitLeft || floatingPlatform.hitRight || floatingPlatform.hitFront || floatingPlatform.hitBack)
            {
                floatingPlatform.DisplayHintText();
            }
            switch (dir)
            {
                case FloatingPlatform.MoveDir.Left:
                    {
                        floatingPlatform.currentDir = FloatingPlatform.MoveDir.Left;
                        break;
                    }
                case FloatingPlatform.MoveDir.Right:
                    {
                        floatingPlatform.currentDir = FloatingPlatform.MoveDir.Right;
                        break;
                    }
                case FloatingPlatform.MoveDir.Forward:
                    {
                        floatingPlatform.currentDir = FloatingPlatform.MoveDir.Forward;
                        break;
                    }
                case FloatingPlatform.MoveDir.Backward:
                    {
                        floatingPlatform.currentDir = FloatingPlatform.MoveDir.Backward;
                        break;
                    }
                default:
                    break;
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (!GetComponent<MovePlatformPressurePlate>().isActiveAndEnabled) return;
        floatingPlatform.FadingHintText();
        floatingPlatform.currentDir = FloatingPlatform.MoveDir.Idle;
    }
}
