using UnityEngine;

public class UIPressButton : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private Animator animator;
    private bool isShowing = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void Show()
    {
        if (isShowing) return;
        isShowing = true;
        animator.SetBool("HaveItem", true);
    }

    public void Hide()
    {
        if (!isShowing) return;
        isShowing = false;
        animator.SetBool("HaveItem", false);
    }

    public void UpdateTarget(Transform t)
    {
        target = t;
    }

}
