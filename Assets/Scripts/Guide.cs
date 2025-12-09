using UnityEngine;

public class Guide : MonoBehaviour
{
    #region Variables

    public GameObject guideUI;
    private Animator animator;

    #endregion

    #region Monobehavior methods

    private void Start()
    {
        animator = guideUI.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guideUI.SetActive(true);
            if (animator != null)
            {
                animator.SetBool("clicking", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guideUI.SetActive(false);
            if (animator != null)
            {
                animator.SetBool("clicking", false);
            }
        }
    }

    #endregion
    #region Public methods
    #endregion
    #region Private methods
    #endregion
}
