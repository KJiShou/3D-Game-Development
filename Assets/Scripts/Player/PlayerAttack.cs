using StarterAssets;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;
    public Transform player;
    public float playerForce = 1.0f;

    private void OnTriggerStay(Collider other)
    {
        if (!thirdPersonController.isAttacking) return;

        TreeInteraction tree = other.GetComponent<TreeInteraction>();
        if (tree != null)
        {
            tree.HitTree();
            tree.PushTree(player);
        }

        TreeLogPush treeLog = other.GetComponent<TreeLogPush>();
        if (treeLog != null)
        {
            Vector3 pushDir = thirdPersonController.transform.forward;
            treeLog.Push(pushDir, playerForce);
        }

        StoneInteract stone = other.GetComponent<StoneInteract>();
        if (stone != null)
        {
            Vector3 pushDir = thirdPersonController.transform.forward;
            stone.Push(pushDir, playerForce);
        }
    }
}
