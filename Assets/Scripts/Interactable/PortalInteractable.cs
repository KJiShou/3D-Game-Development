using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

public class PortalInteractable : MonoBehaviour
{
    public GameObject teleportButton;
    private Animator teleportButtonAnimator;
    private bool havePlayer;

    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportButtonAnimator = teleportButton.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (havePlayer && Input.GetKeyDown(KeyCode.E))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Tutorial")
            {
                Game.GameManager.instance.PassTutorial();
            }
            havePlayer = false;
            teleportButtonAnimator.SetBool("HaveItem", false);
            if (goNextLevel)
            {
                SceneController.instance.NextLevel();
            } else
            {
                SceneController.instance.LoadScene(levelName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = true;
            teleportButtonAnimator.SetBool("HaveItem", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            havePlayer = false;
            teleportButtonAnimator.SetBool("HaveItem", false);
        }
    }
}
