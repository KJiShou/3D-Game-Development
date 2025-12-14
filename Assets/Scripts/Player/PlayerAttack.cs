using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;
    public Transform player;
    public float playerForce = 1.0f;
    public AudioClip pushWoodSound;
    public AudioClip pushPumpkinSound;
    public AudioClip pushStoneSound;

    private AudioSource audioSource;

    private bool soundPlayedThisAttack = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!thirdPersonController.isAttacking)
            soundPlayedThisAttack = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!thirdPersonController.isAttacking)
            return;

        // --- Tree Interaction ---
        TreeInteraction tree = other.GetComponent<TreeInteraction>();
        if (tree != null)
        {
            tree.HitTree();
            tree.PushTree(player);
        }

        // --- Tree Log Interaction ---
        TreeLogPush treeLog = other.GetComponent<TreeLogPush>();
        if (treeLog != null)
        {
            Vector3 pushDir = thirdPersonController.transform.forward;
            treeLog.Push(pushDir, playerForce);

            if (!soundPlayedThisAttack && pushWoodSound != null)
            {
                audioSource.PlayOneShot(pushWoodSound);
                soundPlayedThisAttack = true;
            }
        }

        // --- Stone Interaction ---
        StoneInteract stone = other.GetComponent<StoneInteract>();
        if (stone != null)
        {
            Vector3 pushDir = thirdPersonController.transform.forward;
            stone.Push(pushDir, playerForce);

            if (!soundPlayedThisAttack && pushPumpkinSound != null && other.CompareTag("Pumpkin"))
            {
                audioSource.PlayOneShot(pushPumpkinSound);
                soundPlayedThisAttack = true;
            }

            if (!soundPlayedThisAttack && pushStoneSound != null && !other.CompareTag("Pumpkin"))
            {
                audioSource.PlayOneShot(pushStoneSound);
                soundPlayedThisAttack = true;
            }
        }
    }
}
