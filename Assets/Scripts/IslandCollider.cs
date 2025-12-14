using UnityEngine;

public class IslandCollider : MonoBehaviour
{
    public Transform targetPos;
    private string tagName = "Triggered Stone";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagName))
        {
            other.transform.position = targetPos.position;
        }
    }
}
