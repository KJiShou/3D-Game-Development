using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyMachine : MonoBehaviour
{
    public GameObject openButton;
    public GameObject mainHQ;
    public GameObject winIsland;
    private Animator openButtonAnimator;
    private Animator animator;
    public static bool havePlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        openButtonAnimator = openButton.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E))
        {
            havePlayer = false;
            openButtonAnimator.SetBool("HaveItem", false);
            animator.SetBool("Destroy", true);
            openButton.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(mainHQ, 10);
            StartCoroutine(createWinIsland());
            
        }
    }

    IEnumerator createWinIsland()
    {
        yield return new WaitForSeconds(10);
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
}
