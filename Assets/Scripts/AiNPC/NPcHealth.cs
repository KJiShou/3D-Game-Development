using UnityEngine;

public class NPcHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
    }
}
