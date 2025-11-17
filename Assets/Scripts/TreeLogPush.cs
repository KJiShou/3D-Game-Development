using UnityEngine;

public class TreeLogPush : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction, float force)
    {
        direction.y = 0;
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}