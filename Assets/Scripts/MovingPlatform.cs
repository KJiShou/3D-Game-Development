using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 3f;
    public bool canMove = false;

    private bool playerOnPlatform = false;
    private Vector3 lastPlatformPosition;
    private CharacterController playerCC;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        Vector3 target = canMove && playerOnPlatform ? endPoint.position : startPoint.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        lastPlatformPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            other.transform.SetParent(transform);
        }
        if (other.CompareTag("BuildTree"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            other.transform.SetParent(null);
        }
        if (other.CompareTag("BuildTree"))
        {
            other.transform.SetParent(null);
        }
    }

    public void SetCanMove()
    {
        canMove = true;
    }
}