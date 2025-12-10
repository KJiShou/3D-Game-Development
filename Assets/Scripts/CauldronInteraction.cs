using Game;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CauldronInteraction : MonoBehaviour
{
    public GameObject beforeInteractiveObject;
    public GameObject afterInteractiveObject;
    public ParticleSystem breakParticle;

    public GameObject openButton;
    private Animator openButtonAnimator;

    public Transform pumpkinTarget;

    private GameObject pumpkin;
    bool havePumpkin = false;
    public bool actived = false; 
    bool isMovingPumpkin = false;

    public float moveDuration = 1.0f;

    public UnityEvent OnRouteCompleted;
    private GameManager gameManager;
    private StartMessage message;

    void Start()
    {
        openButtonAnimator = openButton.GetComponent<Animator>();
        openButton.SetActive(false);
        gameManager = GameManager.instance;
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
    }

    void Update()
    {
        if (!actived && havePumpkin && !isMovingPumpkin && gameManager.IsKakaSave() && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MovePumpkinAndCook());
            StartCoroutine(Wait());
            
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(moveDuration);
        message.ShowMessage("Thanks! Use this portal to go to your destination");
        OnRouteCompleted.Invoke();
    }

    private IEnumerator MovePumpkinAndCook()
    {
        isMovingPumpkin = true;

        if (pumpkin == null || pumpkinTarget == null)
            yield break;

        var rb = pumpkin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Vector3 startPos = pumpkin.transform.position;
        Quaternion startRot = pumpkin.transform.rotation;
        Vector3 startScale = pumpkin.transform.localScale;
        Vector3 endScale = Vector3.zero;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            float lerpT = Mathf.Clamp01(t);

            pumpkin.transform.position = Vector3.Lerp(startPos, pumpkinTarget.position, lerpT);
            pumpkin.transform.rotation = Quaternion.Slerp(startRot, pumpkinTarget.rotation, lerpT);

            pumpkin.transform.localScale = Vector3.Lerp(startScale, endScale, lerpT);

            yield return null;
        }

        pumpkin.transform.position = pumpkinTarget.position;
        pumpkin.transform.localScale = Vector3.zero;

        PlayBreakEffect();
        beforeInteractiveObject.SetActive(false);
        afterInteractiveObject.SetActive(true);

        actived = true;

        GetComponent<BoxCollider>().enabled = false;
        openButton.SetActive(false);

        pumpkin.SetActive(false);
        isMovingPumpkin = false;
    }

    void PlayBreakEffect()
    {
        if (breakParticle != null)
        {
            ParticleSystem fx = Instantiate(
                breakParticle,
                afterInteractiveObject.transform.position,
                Quaternion.identity
            );

            fx.Play();
            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetimeMultiplier);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (actived) return;

        if (other.CompareTag("Pumpkin") && gameManager.IsKakaSave())
        {
            openButton.SetActive(true);
            openButtonAnimator.SetBool("HaveItem", true);

            havePumpkin = true;
            pumpkin = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (actived) return;

        if (other.CompareTag("Pumpkin") && gameManager.IsKakaSave())
        {
            openButtonAnimator.SetBool("HaveItem", false);
            openButton.SetActive(false);

            havePumpkin = false;
            pumpkin = null;
        }
    }
}
