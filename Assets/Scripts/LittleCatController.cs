using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class LittleCatController : MonoBehaviour
{
    Animator animator;
    public StarterAssets.StarterAssetsInputs input;
    public bool isAttacking = false;
    private ThirdPersonController thirdPersonController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        // new input system
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !isAttacking || Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("attack");
            input.MoveInput(new Vector2(0, 0));
        }

        float moveX = Input.GetAxis("Horizontal");

        float moveY = Input.GetAxis("Vertical");
        float speed = new Vector2(moveX, moveY).magnitude ;
        animator.SetFloat("Speed", speed * thirdPersonController.MoveSpeed);

        if (input.sprint)
        {
            animator.SetFloat("Speed", speed * thirdPersonController.SprintSpeed);
        }

        if (input.jump)
        {
            animator.SetTrigger("jump");
        }

    }

    public void AttackFinished()
    {
        isAttacking = false;
        input.MoveInput(new Vector2(Input.GetAxis("Horizontal") * thirdPersonController.MoveSpeed, Input.GetAxis("Vertical") * thirdPersonController.MoveSpeed));
    }
}
