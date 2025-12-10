using UnityEngine;
using TMPro;

public class StartMessage : MonoBehaviour
{
    public TMP_Text messageText;
    public float showDuration = 3f;
    public float fadeSpeed = 1f;
    public bool showWhenStart = false;

    private float timer;
    private bool fading = false;

    private void Start()
    {
        if (showWhenStart) ShowMessage(messageText.text);
    }

    public void ShowMessage(string textContent = "")
    {
        gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(textContent))
        {
            messageText.text = textContent;
        }

        messageText.alpha = 1f;
        timer = showDuration;
        fading = false;
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