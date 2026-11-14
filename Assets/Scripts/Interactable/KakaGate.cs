using Game;
using UnityEngine;

public class KakaGate : MonoBehaviour
{
    private Animator animator;
    public GameObject openButton;
    public GameObject kaka;
    public GameObject destroyMachine;
    private Animator openButtonAnimator;

    public bool havePlayer = false;
    public bool hasKey = false;
    public bool isKaka = true;
    public string showMessage = "Kaka's been locked up! Go rescue him!";
    public string needKeyMessage = "You need a key to open this gate!";

    private StartMessage message;
    private GameManager gameManager;
    private bool gateOpened;

    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
        gameManager = GameManager.instance;

        // The final machine starts inactive in Level3. Restore its unlock when
        // replaying/reloading with an already-saved rescue, not only on interaction.
        if (destroyMachine != null &&
            (isKaka ? gameManager.IsKakaSave() : gameManager.IsWawaSave()))
        {
            OpenGate(true);
        }
    }

    void Update()
    {
        if (gateOpened || !havePlayer) return;

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

    private void OpenGate(bool restoring = false)
    {
        if (gateOpened) return;
        gateOpened = true;
        if (destroyMachine != null) destroyMachine.SetActive(true);

        havePlayer = false;
        openButtonAnimator.SetBool("HaveItem", false);

        animator.SetBool("Save", true);
        if (restoring)
        {
            // Do not replay the rescue or leave the rescued character in the cage.
            if (kaka != null) kaka.SetActive(false);
        }
        else
        {
            Destroy(kaka, 10);
        }

        openButton.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;

        if (!restoring)
        {
            if (isKaka) gameManager.SaveKaka();
            else gameManager.SaveWawa();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gateOpened || !other.CompareTag("Player")) return;

        havePlayer = true;
        openButtonAnimator.SetBool("HaveItem", true);

        message.ShowMessage(showMessage);
    }

    private void OnTriggerExit(Collider other)
    {
        if (gateOpened || !other.CompareTag("Player")) return;

        havePlayer = false;
        openButtonAnimator.SetBool("HaveItem", false);
    }
}
