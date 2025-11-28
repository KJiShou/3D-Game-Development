using UnityEngine;

public class BuildPoint : MonoBehaviour

{

    public GameObject buildButton;

    public GameObject bridge;

    public GameObject nextStageFog;

    public bool haveItem = false;

    private Animator buildButtonAnimator;

    private GameObject treeLog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {

        buildButtonAnimator = buildButton.GetComponent<Animator>();

    }



    // Update is called once per frame

    void Update()

    {

        if (haveItem && treeLog != null && Input.GetKeyDown(KeyCode.E))

        {

            if (bridge != null) bridge.SetActive(true);

            treeLog.SetActive(false);

            gameObject.SetActive(false);

            if (nextStageFog != null) nextStageFog.SetActive(false);

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

    }



    private void OnTriggerExit(Collider other)

    {

        if (other.gameObject.tag == "BuildTree")

        {

            haveItem = false;

            buildButtonAnimator.SetBool("HaveItem", false);

            treeLog = null;

        }

    }

}