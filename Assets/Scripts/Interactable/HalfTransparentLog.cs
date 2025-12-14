using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class HalfTransparentLog : MonoBehaviour
{
    public GameObject triggeredObject;
    private string targetTag = "SmallTree";
    private static int buildCount = 0;
    private static List<GameObject> trees = new List<GameObject>();
    private static List<Collider> treesCollider = new List<Collider>();
    private Collider thisCollider;

    public AudioClip popSound;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        for (int i = 0; i < trees.Count; i++)
        {

            if (thisCollider == null || trees[i] == null || treesCollider[i] == null) continue;

            // XZ boundary
            float thisMinX = thisCollider.bounds.min.x;
            float thisMaxX = thisCollider.bounds.max.x;
            float thisMinZ = thisCollider.bounds.min.z;
            float thisMaxZ = thisCollider.bounds.max.z;

            float treeMinX = treesCollider[i].bounds.min.x;
            float treeMaxX = treesCollider[i].bounds.max.x;
            float treeMinZ = treesCollider[i].bounds.min.z;
            float treeMaxZ = treesCollider[i].bounds.max.z;

            // calc x and z are overlapping
            float overlapX = Mathf.Max(0, Mathf.Min(thisMaxX, treeMaxX) - Mathf.Max(thisMinX, treeMinX));
            float overlapZ = Mathf.Max(0, Mathf.Min(thisMaxZ, treeMaxZ) - Mathf.Max(thisMinZ, treeMinZ));

            // if overlapping > 50%, then transform log to this gameObject position
            float overlapPercentX = overlapX / (treeMaxX - treeMinX);
            float overlapPercentZ = overlapZ / (treeMaxZ - treeMinZ);

            if (overlapPercentX >= 0.5f && overlapPercentZ >= 0.5f)
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                trees[i].transform.position = newPos;
                Rigidbody rb = trees[i].GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                gameObject.GetComponent<HalfTransparentLog>().enabled = false;
            }
        }
    }


    private void OnDisable()
    {
        buildCount++;
        if (buildCount >= 4)
        {
            foreach (GameObject tree in trees)
            {
                tree.SetActive(false);
            }
            triggeredObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !trees.Contains(other.gameObject))
        {
            trees.Add(other.gameObject);
            treesCollider.Add(other);
        }

    }
}
