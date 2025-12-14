using UI;
using UnityEngine;

public class ElectricCollect : MonoBehaviour
{
    public static ElectricCollect instance;
    public static int charge = 0; 
    
    public AudioClip collectSound;
    public float volume = 1.5f;

    private UIManager uiManager;

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
            charge++;
            uiManager.UpdateThunderCount();
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public int GetCharge()
    {
        return charge;
    }
}