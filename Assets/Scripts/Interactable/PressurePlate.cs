using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject portal;
    private bool isPressed;
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed) portal.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = true;
            animator.SetBool("isPressed", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = false;
            animator.SetBool("isPressed", false);
        }
    }
}
