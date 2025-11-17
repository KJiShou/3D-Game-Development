using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public GameObject afterInteract;
    public GameObject treeLog;
    public GameObject treeModel;
    private MeshRenderer treeMeshRenderer;
    private BoxCollider treeBoxCollider;

    public float pushDistance = 0.5f;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeMeshRenderer = treeModel.GetComponent<MeshRenderer>();
        treeBoxCollider = treeModel.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitTree()
    {
        // disable tree
        if (treeModel != null)
        {
            treeMeshRenderer.enabled = false;
            treeBoxCollider.enabled = false;
        }

        // show tree log and root
        if (afterInteract != null) afterInteract.SetActive(true);
    }

    public void PushTree(Transform player)
    {
        if (player == null) return;

        Vector3 forward = player.forward;
        forward.y = 0;

        Vector3 targetPos = treeLog.GetComponent<Transform>().position + forward.normalized * pushDistance;

        treeLog.transform.position = targetPos;
    }
}
