using Cinemachine;
using Game;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class DestroyMachine : MonoBehaviour
{
    public GameObject openButton;
    public GameObject mainHQ;
    public GameObject winIsland;
    private Animator openButtonAnimator;
    private Animator animator;
    public bool havePlayer;
    public AudioClip explosionSound;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.UpdateCurrentLevel();
            StartCoroutine(PlayLoopFor9Seconds());
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
            animator.SetBool("Destroy", true);
            openButton.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(mainHQ, 10);
            StartCoroutine(createWinIsland());
            CameraShake.Instance.ShakeCamera(5f, 10.0f, 10.0f);
        }
    }

    IEnumerator createWinIsland()
    {
        yield return new WaitForSeconds(10);
        SceneController.instance.LoadScene("EndScene");
        winIsland.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = true;
            openButtonAnimator.SetBool("HaveItem", true);
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

    IEnumerator PlayLoopFor9Seconds()
    {
        audioSource.loop = true;
        audioSource.Play();

        yield return new WaitForSeconds(9f);

        audioSource.Stop();
    }
}
