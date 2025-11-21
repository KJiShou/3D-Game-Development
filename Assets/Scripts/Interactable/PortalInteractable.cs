using UnityEngine;

public class PortalInteractable : MonoBehaviour
{
    public GameObject teleportButton;
    private Animator teleportButtonAnimator;
    private bool havePlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportButtonAnimator = teleportButton.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = true;
            teleportButtonAnimator.SetBool("HaveItem", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = false;
            teleportButtonAnimator.SetBool("HaveItem", false);
        }
    }
}
