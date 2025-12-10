using Game;
using UnityEngine;

public class WitchText : MonoBehaviour
{
    private StartMessage message;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameManager.IsKakaSave())
            {
                message.ShowMessage("I need pumpkin to cook my soup!!!");
            }
            else
            {
                message.ShowMessage("Hey!! Your friend still locked up!!!");
            }

        }
    }
}
