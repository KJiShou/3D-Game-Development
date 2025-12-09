using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public GameObject buildPointMsg;
    public GameObject guidePoint1;
    public GameObject guidePoint1UI;
    public GameObject newMsg;
    public GameObject[] trees;
    private bool hitFirstTree = false;

    private void Update()
    {
        if (!hitFirstTree)
        {
            foreach (GameObject tree in trees)
            {
                TreeInteraction treeInteraction = tree.GetComponent<TreeInteraction>();
                if (treeInteraction != null && treeInteraction.getHit)
                {
                    buildPointMsg.SetActive(true);
                    guidePoint1.SetActive(false);
                    guidePoint1UI.SetActive(false);
                    hitFirstTree = true;
                    newMsg.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        newMsg.SetActive(false);
    }
}
