using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Activate Object")]
    [Tooltip("Need to activate the object or not")]
    public GameObject activeObject;
    public bool triggeredActivate = false;
    public bool triggeredStatus = true;

    [Header("Activate Object animator")]
    [Tooltip("Need to activate the object or not")]
    public Animator activeObjectAnim;
    public bool triggeredAnimation = false;
    public string animatorTrigger = "";

    public bool isPressed;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animatorTrigger.Length <= 0)
        {
            animatorTrigger = "isPressed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed && triggeredActivate) activeObject.SetActive(triggeredStatus);
        if (!isPressed && triggeredActivate) activeObject.SetActive(!triggeredStatus);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = true;
            animator.SetBool("isPressed", true);
            activeObjectAnim.SetBool(animatorTrigger, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Triggered Stone")
        {
            isPressed = false;
            animator.SetBool("isPressed", false);
            activeObjectAnim.SetBool(animatorTrigger, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPressed = true;
            animator.SetBool("isPressed", true);
            activeObjectAnim.SetBool(animatorTrigger, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPressed = false;
            animator.SetBool("isPressed", false);
            activeObjectAnim.SetBool(animatorTrigger, false);
        }
    }
}
