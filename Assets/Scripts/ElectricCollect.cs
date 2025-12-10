using UnityEngine;

public class ElectricCollect : MonoBehaviour
{
    public static int charge = 0; 
    
    public AudioClip collectSound;
    public float volume = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ✨ 玩家收集到物品时，增加 charge 的值
            charge++; 
            
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            Destroy(gameObject);
        }
    }
}