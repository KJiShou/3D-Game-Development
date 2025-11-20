using UnityEngine;

public class StoneInteract : MonoBehaviour
{
    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction, float force)
    {
        direction.y = 0.5f;
        rigidBody.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}