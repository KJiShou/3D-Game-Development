using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    private AudioSource audioSource;
    public AudioClip teleportSound;

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAnim(sceneName));
    }

    public void LoadAnimation()
    {
        StartCoroutine(LoadAnim());
    }

    IEnumerator LoadSceneAnim(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(0.2f);
        transitionAnim.SetTrigger("Start");
        if(sceneName != "MainMenu") audioSource.PlayOneShot(teleportSound);
    }

    IEnumerator LoadAnim()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.2f);
        transitionAnim.SetTrigger("Start");
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
        if (SceneManager.GetActiveScene().ToString() != "MainMenu") audioSource.PlayOneShot(teleportSound);
    }
}
