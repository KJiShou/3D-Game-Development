using UnityEngine;
using TMPro;

public class StartMessage : MonoBehaviour
{
    public TMP_Text messageText;
    public float showDuration = 3f;  
    public float fadeSpeed = 1f;  

    private float timer;
    private bool fading = false;

    private void Start()
    {
        messageText.alpha = 1f;
        timer = showDuration;
    }

    private void Update()
    {
        if (!fading)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                fading = true;
            }
        }
        else
        {
            messageText.alpha -= Time.deltaTime * fadeSpeed;

            if (messageText.alpha <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
