using UnityEngine;

public class RotateAndFloat : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}
