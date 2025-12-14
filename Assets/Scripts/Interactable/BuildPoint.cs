using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildPoint : MonoBehaviour

{
    private Scene currentScene;

    public GameObject buildButton;

    public GameObject bridge;

    public GameObject nextStageFog;

    public ParticleSystem buildEffect;

    public bool haveItem = false;

    private Animator buildButtonAnimator;

    private GameObject treeLog;

    private bool isInteract = false;

    public GameObject hint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {
        //currentScene = SceneManager.GetActiveScene();
        //if (currentScene.name == "Tutorial") buildButtonAnimator = buildButton.GetComponent<Animator>();
        if (buildButton != null) buildButtonAnimator = buildButton.GetComponent<Animator>();

    }



    // Update is called once per frame

    void Update()

    {

        if (haveItem && treeLog != null && Input.GetKeyDown(KeyCode.E))
        {

            if (bridge != null) bridge.SetActive(true);
            PlayBreakEffect();
            treeLog.SetActive(false);

            gameObject.SetActive(false);

            if (nextStageFog != null) nextStageFog.SetActive(false);

        }

        if (haveItem && hint != null && Input.GetKeyDown(KeyCode.E))
        {
            isInteract = !isInteract;
            if (isInteract)
            {
                buildButton.SetActive(false);
                buildButtonAnimator.SetBool("HaveItem", false);
            }
            else
            {
                buildButton.SetActive(true);
                buildButtonAnimator.SetBool("HaveItem", true);
            }
            hint.SetActive(isInteract);
        }

    }



    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.tag == "BuildTree")
        {

            haveItem = true;

            buildButtonAnimator.SetBool("HaveItem", true);

            treeLog = other.gameObject;

        }
        if (gameObject.tag == "Interactable" && other.gameObject.tag == "Player")
        {
            haveItem = true;

            buildButtonAnimator.SetBool("HaveItem", true);
        }

    }



    private void OnTriggerExit(Collider other)

    {

        if (other.gameObject.tag == "BuildTree")

        {

            haveItem = false;

            buildButtonAnimator.SetBool("HaveItem", false);

            treeLog = null;

        }
        if (gameObject.tag == "Interactable" && other.gameObject.tag == "Player")
        {
            haveItem = false;

            buildButtonAnimator.SetBool("HaveItem", false);
        }

    }

    void PlayBreakEffect()
    {
        if (buildEffect != null)
        {
            ParticleSystem fx = Instantiate(buildEffect, bridge.transform.position, Quaternion.identity);

            fx.Play();

            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetimeMultiplier);
        }
    }

}