
using Game;
using System.Collections;
using UI;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        private GameManager gameManager;
        private UIManager uiManager;
        private Scene currentScene;

        public bool isAttacking = false;

        public SkinnedMeshRenderer skinnedMeshRenderer;
        public Material idle;
        public Material happy;
        public Material cry;
        public Material angry;

        public bool getChasing;

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        public bool isSprint
        {
            get { return _input.sprint; }
        }

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioSource attackSFXAudioSource;
        public AudioSource walkSFXAudioSource;
        public AudioClip LandingAudioClip;
        public AudioClip JumpAudioClip;
        public AudioClip attackAudioClip;
        public AudioClip dieAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Header("Footstep Material Sounds")]
        public AudioClip[] grassFootsteps;
        public AudioClip[] dirtFootsteps;
        public AudioClip[] woodFootsteps;
        public AudioClip[] snowFootsteps;

        [Header("Footstep Material Settings")]
        [Range(0,1f)] public float grassVolume = 0.7f;
        public float grassPitch = 1.1f;

        [Range(0,1f)] public float woodVolume = 0.9f;
        public float woodPitch = 0.95f;

        [Range(0,1f)] public float snowVolume = 0.7f;
        public float snowPitch = 1.1f;

        [Range(0,1f)] public float dirtVolume = 0.9f;
        public float dirtPitch = 0.95f;

        public bool _jump = false;
        private bool wasGrounded = true;
        private float _lastJumpSoundTime = -1f;
        public float JumpSoundCooldown = 0.1f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private bool _die = false;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDAttack;
        private int _animIDDie;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            if (_controller == null)
            {
                _controller = GetComponent<CharacterController>();   
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            gameManager = GameManager.instance;
            uiManager = UIManager.instance;
            currentScene = SceneManager.GetActiveScene();
        }

        private void Update()
        {
            if (_die)
            {
                skinnedMeshRenderer.material = cry;
                return;
            }
            // new input system
            if (Mouse.current != null && Mouse.current.leftButton.isPressed && !isAttacking || UnityEngine.Input.GetMouseButtonDown(0) && !isAttacking)
            {
                Attack();
            }

            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
            if (getChasing && skinnedMeshRenderer.material != cry) skinnedMeshRenderer.material = cry;
            if (!getChasing && skinnedMeshRenderer.material != idle) skinnedMeshRenderer.material = idle;

            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }

            _verticalVelocity += Gravity * Time.deltaTime;
            _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDAttack = Animator.StringToHash("Attack");
            _animIDDie = Animator.StringToHash("Die");
        }

        public void Bounce(float force)
        {
            _verticalVelocity = force;
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // ---- Landing Sound ----
            if (!wasGrounded && Grounded)
            {
                walkSFXAudioSource.pitch = 1.0f;
                walkSFXAudioSource.volume = 1.0f;
                if (LandingAudioClip)
                {
                    //walkSFXAudioSource.pitch = 0.3f;
                    //walkSFXAudioSource.PlayOneShot(LandingAudioClip);
                    //AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }

            // store grounded state for next frame
            wasGrounded = Grounded;

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;

            }
            else if (!LockCameraPosition)
            {
                Vector2 look = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));

                if (look.sqrMagnitude >= _threshold)
                {
                    float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                    _cinemachineTargetYaw += look.x;
                    _cinemachineTargetPitch -= look.y;
                }

            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }



        private void Move()
        {
            if (!_die)
            {
                // set target speed based on move speed, sprint speed and if sprint is pressed
                float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

                // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

                // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
                // if there is no input, set the target speed to 0
                if (_input.move == Vector2.zero) targetSpeed = 0.0f;

                // a reference to the players current horizontal velocity
                float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

                float speedOffset = 0.1f;
                float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

                // accelerate or decelerate to target speed
                if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                    currentHorizontalSpeed > targetSpeed + speedOffset)
                {
                    // creates curved result rather than a linear one giving a more organic speed change
                    // note T in Lerp is clamped, so we don't need to clamp our speed
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                        Time.deltaTime * SpeedChangeRate);

                    // round speed to 3 decimal places
                    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                }
                else
                {
                    _speed = targetSpeed;
                }

                _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
                if (_animationBlend < 0.01f) _animationBlend = 0f;

                // normalise input direction
                Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

                // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
                // if there is a move input rotate player when the player is moving
                if (_input.move != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                      _mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }


                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                // move the player
                _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
                }
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;
                
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && Grounded)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    if (Time.time - _lastJumpSoundTime > JumpSoundCooldown)
                    {
                        _lastJumpSoundTime = Time.time;
                        if (JumpAudioClip)
                        {
                            walkSFXAudioSource.pitch = 0.7f;
                            walkSFXAudioSource.volume = 0.2f;
                            walkSFXAudioSource.PlayOneShot(JumpAudioClip);
                            //AudioSource.PlayClipAtPoint(JumpAudioClip, transform.position, FootstepAudioVolume);
                        }
                    }

                    if (_hasAnimator)
                    {
                        _animator.SetTrigger(_animIDJump);
                    }

                    _input.jump = false;
                }
                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            // if (animationEvent.animatorClipInfo.weight > 0.5f)
            // {
            //     if (FootstepAudioClips.Length > 0)
            //     {
            //         var index = Random.Range(0, FootstepAudioClips.Length);
            //         if (FootstepAudioClips[index])
            //         {
            //             //walkSFXAudioSource.pitch = 0.5f;
            //             //walkSFXAudioSource.volume = 0.5f;
            //             walkSFXAudioSource.PlayOneShot(FootstepAudioClips[index]);
            //             // AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
            //         }
            //     }
            // }
            if (animationEvent.animatorClipInfo.weight > 0.5f)
    {
        AudioClip clipToPlay = null;
        RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            switch (hit.collider.tag)
            {
                case "Grass":
                    if (grassFootsteps.Length > 0)
                        clipToPlay = grassFootsteps[Random.Range(0, grassFootsteps.Length)];
                    break;
                case "Dirt":
                    if (dirtFootsteps.Length > 0)
                        clipToPlay = dirtFootsteps[Random.Range(0, dirtFootsteps.Length)];
                    break;
                case "Wood":
                    if (woodFootsteps.Length > 0)
                        clipToPlay = woodFootsteps[Random.Range(0, woodFootsteps.Length)];
                    break;
                case "Snow":
                    if (snowFootsteps.Length > 0)
                        clipToPlay = snowFootsteps[Random.Range(0, snowFootsteps.Length)];
                    break;
                default:
                    if (FootstepAudioClips.Length > 0)
                        clipToPlay = FootstepAudioClips[Random.Range(0, FootstepAudioClips.Length)];
                    break;
            }
        }

        if (clipToPlay != null)
{
    switch (hit.collider.tag)
    {
        case "Grass":
            walkSFXAudioSource.pitch = grassPitch;
            walkSFXAudioSource.PlayOneShot(clipToPlay, grassVolume);
            break;

        case "Wood":
            walkSFXAudioSource.pitch = woodPitch;
            walkSFXAudioSource.PlayOneShot(clipToPlay, woodVolume);
            break;

        case "Snow":
            walkSFXAudioSource.pitch = snowPitch;
            walkSFXAudioSource.PlayOneShot(clipToPlay, snowVolume);
            break;

        case "Dirt":
            walkSFXAudioSource.pitch = dirtPitch;
            walkSFXAudioSource.PlayOneShot(clipToPlay, dirtVolume);
            break;

        default:
            walkSFXAudioSource.pitch = 1f;
            walkSFXAudioSource.PlayOneShot(clipToPlay, FootstepAudioVolume);
            break;
    }
}

    }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (LandingAudioClip)
                {
                    //walkSFXAudioSource.pitch = 1.0f;
                    walkSFXAudioSource.PlayOneShot(LandingAudioClip);
                    //AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        void Attack()
        {
            isAttacking = true;
            _animator.SetTrigger(_animIDAttack);
            if (attackAudioClip)
            {
                attackSFXAudioSource.volume = 0.2f;
                attackSFXAudioSource.PlayOneShot(attackAudioClip);
                //AudioSource.PlayClipAtPoint(attackAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public void AttackFinished()
        {
            isAttacking = false;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if ((hit.collider.CompareTag("DiePlane") || hit.collider.name == "Ghost") && !_die)
            {
                if (!_die && dieAudioClip)
                {
                    attackSFXAudioSource.volume = 1.0f;
                    attackSFXAudioSource.PlayOneShot(dieAudioClip);
                    //AudioSource.PlayClipAtPoint(dieAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
                _die = true;
                _animator.SetBool(_animIDDie, true);
                skinnedMeshRenderer.material = cry;
                gameManager.DeductLife();
                uiManager.UpdateLeftLife();
                StartCoroutine(WaitRestartLevel());
            }
        }

        IEnumerator WaitRestartLevel()
        {
            if (GameManager.instance.GetLeftLife() <5)
            {
                if (RespawnManager.instance.currentRespawn == null)
                {
                    yield return new WaitForSeconds(1);
                    GameManager.instance.RestartLevel(currentScene.name);
                }
                else
                {
                    yield return new WaitForSeconds(1);
                    SceneController.instance.LoadAnimation();
                    yield return new WaitForSeconds(0.75f);
                    _animator.SetBool(_animIDDie, false);
                    yield return new WaitForSeconds(0.45f);
                    RespawnManager.instance.RespawnPlayer(gameObject);
                    _die = false;
                    skinnedMeshRenderer.material = idle;

                }
            }else
            {
                SceneController.instance.LoadScene("loseScene");
            }

        }
        
    }
}
