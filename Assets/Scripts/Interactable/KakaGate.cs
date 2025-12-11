using Game;
using UnityEngine;

public class KakaGate : MonoBehaviour
{
    private Animator animator;
    public GameObject openButton;
    public GameObject kaka;
    public GameObject destroyMachine;
    private Animator openButtonAnimator;
    public bool havePlayer;
    private StartMessage message;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E))
        {
            destroyMachine.SetActive(true);
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
            animator.SetBool("Save", true);
            Destroy(kaka, 6);
            openButton.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameManager.SaveKaka();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = true;
            openButtonAnimator.SetBool("HaveItem", true);
            message.ShowMessage("Kaka's been locked up! Go rescue him!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
        }
    }
}
