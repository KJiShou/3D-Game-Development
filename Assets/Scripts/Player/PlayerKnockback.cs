using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    private CharacterController cc;

    private Vector3 knockbackVelocity;
    private float knockbackDecay = 5f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (knockbackVelocity.magnitude > 0.1f)
        {
            cc.Move(knockbackVelocity * Time.deltaTime);

            knockbackVelocity = Vector3.Lerp(
                knockbackVelocity,
                Vector3.zero,
                knockbackDecay * Time.deltaTime
            );
        }
    }

    public void AddKnockback(Vector3 force)
    {
        knockbackVelocity = force;
    }
}
