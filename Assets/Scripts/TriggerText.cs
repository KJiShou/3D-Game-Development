using UnityEngine;

public class TriggerText : MonoBehaviour
{
    public string textToDisplay = "";

    private StartMessage message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        message = FindAnyObjectByType<StartMessage>().GetComponent<StartMessage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            message.ShowMessage(textToDisplay);
        }
    }
}
