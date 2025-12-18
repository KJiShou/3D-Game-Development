using Game;
using System.Collections;
using UnityEngine;

public class LoadOtherScene : MonoBehaviour
{
    private bool isPressed = false;
    public float time = 0.0f;
    public string SceneName = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && Input.GetKeyDown(KeyCode.Space))
        {
            isPressed = true;
            if (SceneName == "")
            {
                GameManager.instance.StartGame();
                return;
            }
            if (SceneName == "MainMenu")
            {
                GameManager.instance.UnlockCursor();
            }
                SceneController.instance.LoadScene(SceneName);
        }
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(time);
        if (SceneName == "")
        {
            GameManager.instance.StartGame();
            yield break;
        }
        if (SceneName == "MainMenu")
        {
            GameManager.instance.UnlockCursor();
        }
        SceneController.instance.LoadScene(SceneName);
    }
}
