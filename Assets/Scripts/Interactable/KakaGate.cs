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
    public bool hasKey = false;
    public bool isKaka = true;
    public string showMessage = "Kaka's been locked up! Go rescue him!";
    public string needKeyMessage = "You need a key to open this gate!";

    private StartMessage message;
    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (!havePlayer) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenGate();
        }
    }

    private void TryOpenGate()
    {
        if (!hasKey)
        {
            OpenGate();
            return;
        }

        if (KeyCollect.hasKey)
        {
            OpenGate();
        }
        else
        {
            message.ShowMessage(needKeyMessage);
        }
    }

    private void OpenGate()
    {
        if (destroyMachine != null) destroyMachine.SetActive(true);

        havePlayer = false;
        openButtonAnimator.SetBool("HaveItem", false);

        animator.SetBool("Save", true);
        Destroy(kaka, 10);

        openButton.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;

        if (isKaka) gameManager.SaveKaka();
        if (!isKaka) gameManager.SaveWawa();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        havePlayer = true;
        openButtonAnimator.SetBool("HaveItem", true);

        message.ShowMessage(showMessage);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        havePlayer = false;
        openButtonAnimator.SetBool("HaveItem", false);
    }
}
