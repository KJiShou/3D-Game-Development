using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public GameObject afterInteract;
    public GameObject treeLog;
    public GameObject treeModel;
    private MeshRenderer treeMeshRenderer;
    private BoxCollider treeBoxCollider;
    public ParticleSystem breakParticle;

    public float pushDistance = 0.5f;

    public bool getHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeMeshRenderer = treeModel.GetComponent<MeshRenderer>();
        treeBoxCollider = treeModel.GetComponent<BoxCollider>();
    }

    public void HitTree()
    {
        PlayBreakEffect();
        // disable tree
        if (treeModel != null)
        {
            treeMeshRenderer.enabled = false;
            treeBoxCollider.enabled = false;
        }

        // show tree log and root
        if (afterInteract != null) afterInteract.SetActive(true);
        getHit = true;
    }

    public void PushTree(Transform player)
    {
        if (player == null) return;

        Vector3 forward = player.forward;
        forward.y = 0;

        Vector3 targetPos = treeLog.GetComponent<Transform>().position + forward.normalized * pushDistance;

        treeLog.transform.position = targetPos;
    }

    void PlayBreakEffect()
    {
        if (breakParticle != null)
        {
            ParticleSystem fx = Instantiate(breakParticle, afterInteract.transform.position, Quaternion.identity);

            fx.Play();

            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetimeMultiplier);
        }
    }
}
