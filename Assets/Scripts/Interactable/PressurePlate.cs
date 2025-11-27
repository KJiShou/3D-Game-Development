using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject activeObject;
    public bool triggeredStatus = true;
    public Animator activeObjectAnim;
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
        if (isPressed) activeObject.SetActive(triggeredStatus);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = true;
            animator.SetBool("isPressed", true);
            activeObjectAnim.SetBool("triggered", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = false;
            animator.SetBool("isPressed", false);
            activeObjectAnim.SetBool("triggered", false);
        }
    }
}
