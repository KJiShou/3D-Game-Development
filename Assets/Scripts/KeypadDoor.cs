using NavKeypad;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeypadDoor : MonoBehaviour
{
    public GameObject openButton;
    private Animator openButtonAnimator;
    private Animator animator;
    public bool havePlayer;
    public GameObject player;
    public Cinemachine.CinemachineVirtualCamera keypadCamera;
    private bool success = false;

    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;
    private bool isUsingKeypad = false;
    public int requiredCharge = 4;
    public GameObject morseCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
        controller = player.GetComponent<ThirdPersonController>();
        inputs = player.GetComponent<StarterAssetsInputs>();
    }

    public void StartKeypad()
    {
        isUsingKeypad = true;
        StartCoroutine(CreateMorseCode());

        // Disable player movement
        controller.enabled = false;

        // Unlock cursor
        inputs.cursorLocked = false;
        inputs.cursorInputForLook = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Switch camera
        keypadCamera.Priority = 99;

        player.SetActive(false);
    }

    IEnumerator CreateMorseCode()
    {
        yield return new WaitForSeconds(2f);
        morseCode.SetActive(true);
    }

    public void EndKeypad()
    {
        player.SetActive(true);
        isUsingKeypad = false;
        morseCode.SetActive(false);

        // Enable player movement
        controller.enabled = true;

        // Lock cursor again
        inputs.cursorLocked = true;
        inputs.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Return camera
        keypadCamera.Priority = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E) && !success)
        {
            // if (ElectricCollect.charge < requiredCharge)
            // {
            //     LowElectricMsg.instance.Show("Not enough battery, the keypad cannot be used");
            //     return;
            // }
            
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
            //animator.SetBool("Save", true);
            openButton.SetActive(false);
            StartKeypad();
        }

        if (isUsingKeypad && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            openButton.SetActive(true);
            EndKeypad();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!success && other.gameObject.tag == "Player")
        {
            havePlayer = true;
            openButtonAnimator.SetBool("HaveItem", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!success && other.gameObject.tag == "Player")
        {
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
        }
    }

    public void Success()
    {
        success = true;
        openButton.SetActive(false);
        EndKeypad();
    }
}


