using UnityEngine;
using UI;

public class KeyCollect : MonoBehaviour
{
    public static bool hasKey = false;
    public static KeyCollect instance;
    private UIManager uiManager;

    public AudioClip collectSound;
    public float volume = 1.5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.UpdateKeyUI();
            hasKey = true;
            UIManager.instance.UpdateKeyUI();
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
    }
}
