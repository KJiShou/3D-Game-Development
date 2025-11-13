using UnityEngine;
using UnityEngine.InputSystem;

public class LittleCatController : MonoBehaviour
{
    Animator animator;
    public StarterAssets.StarterAssetsInputs input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            animator.SetTrigger("attack");
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        float speed = new Vector2(moveX, moveY).magnitude;
        animator.SetFloat("Speed", speed);

        if (input.sprint)
        {
            animator.SetFloat("Speed", 2);
        }

        if(input.jump)
        {
            animator.SetTrigger("jump");
        }

    }
}
