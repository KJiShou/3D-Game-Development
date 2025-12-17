using Game;
using System.Collections;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(NextScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.UnlockCursor();
            SceneController.instance.LoadScene("MainMenu");
        }
    }

    IEnumerator NextScene()
    {
        GameManager.instance.UnlockCursor();
        yield return new WaitForSeconds(5);
        SceneController.instance.LoadScene("MainMenu");
    }
}
