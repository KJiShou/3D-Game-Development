using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    public float explosionForce = 25f;
    public float upForce = 10f;

    private void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKnockback kb = other.GetComponent<PlayerKnockback>();
            if (kb != null)
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;
                dir.y = 0.4f;

                Vector3 force = dir * explosionForce + Vector3.up * upForce;

                kb.AddKnockback(force);
            }
        }
    }
}
