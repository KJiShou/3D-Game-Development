using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }else
            {
                Destroy(gameObject);
            }
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAnim(sceneName));
    }

    IEnumerator LoadSceneAnim(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(0.2f);
        transitionAnim.SetTrigger("Start");
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }
}
