using UnityEngine;

public class OrthoCamera : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    void LateUpdate()
    {
        Vector3 targetPos = player.position + new Vector3(0, 10, -5);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.RotateAround(player.position, Vector3.up, mouseX);
    }
    
}
