using UnityEngine;
using UnityEngine.Rendering;

public class GuideManager : MonoBehaviour
{
    public GameObject buildPointMsg;
    public GameObject guidePoint1;
    public GameObject guidePoint1UI;
    public GameObject newMsg;
    public GameObject[] trees;
    public GameObject pressurePlateObj;
    public GameObject congratsMsg;
    public GameObject guidePoint3UI;
    private PressurePlate pressurePlate;
    private bool hitFirstTree = false;
    private bool isFirstPress = true;

    private void Start()
    {
        pressurePlate = pressurePlateObj.GetComponentInChildren<PressurePlate>();
    }

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

        if (pressurePlate != null && pressurePlate.isPressed)
        {
            isFirstPress = false;
            guidePoint3UI.SetActive(false);
            congratsMsg.SetActive(true);
        }

        if (!isFirstPress && !pressurePlate.isPressed)
        {
            guidePoint3UI.SetActive(true);
            congratsMsg.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        newMsg.SetActive(false);
    }
}
