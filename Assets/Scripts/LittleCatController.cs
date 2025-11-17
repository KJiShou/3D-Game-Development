using StarterAssets;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class LittleCatController : MonoBehaviour
{
    Animator animator;
    public StarterAssets.StarterAssetsInputs input;
    public bool isAttacking = false;
    public AudioClip attackAudioClip;
    private ThirdPersonController thirdPersonController;
    private CharacterController _controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackFinished()
    {
        isAttacking = false;
        input.MoveInput(new Vector2(Input.GetAxis("Horizontal") * thirdPersonController.MoveSpeed, Input.GetAxis("Vertical") * thirdPersonController.MoveSpeed));
    }
}
