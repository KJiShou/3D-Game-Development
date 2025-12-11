using CurvedPathGenerator;
using Game;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public string textToDisplay = "";
    public GameObject particle;
    private bool isMoving = false;
    private PathFollower pathFollower;
    private StartMessage message;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
        pathFollower = particle.GetComponent<PathFollower>();
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !gameManager.IsKakaSave())
        {
            message.ShowMessage(textToDisplay);
            if(!isMoving)
            {
                particle.SetActive(true);
                pathFollower.StartFollow();
                isMoving = true;
            }
        }
    }

    public void CloseIsMoving()
    {
        isMoving = false;
    }
}
