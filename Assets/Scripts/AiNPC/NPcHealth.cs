using Unity.Hierarchy;
using UnityEngine;

public class NPcHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Animator _animator;
    SkinnedMeshRenderer skinnedMeshRenderer;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();   
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
        _animator.SetTrigger("Damage");
        blinkTimer = blinkDuration;
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(10);
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = lerp * blinkIntensity;
        Color color = skinnedMeshRenderer.materials[0].GetColor("MainColor");
        color = Color.white * intensity;
    }

    public void SetDisable()
    {
        GameObject.Destroy(gameObject);
    }
}
