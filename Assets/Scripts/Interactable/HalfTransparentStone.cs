using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HalfTransparentStone : MonoBehaviour
{
    public GameObject triggeredObject;
    public LiftPlatform liftPlatform;

    private GameObject stone;
    private Rigidbody stoneRb;

    public float minHeight = 4.7f;
    public float maxHeight = 18.5f;
    private float animateSpeed = 3.0f;
    public bool goUp = false;
    public bool goDown = false;

    private string targetTag = "Triggered Stone";

    public Collider thisCollider;
    private bool isTriggered = false;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
        liftPlatform = triggeredObject.GetComponent<LiftPlatform>();
    }

    private void Update()
    {
        if (thisCollider && stone != null)
        {
            // XZ boundary
            float thisMinX = thisCollider.bounds.min.x;
            float thisMaxX = thisCollider.bounds.max.x;
            float thisMinZ = thisCollider.bounds.min.z;
            float thisMaxZ = thisCollider.bounds.max.z;

            Collider stoneCollider = stone.GetComponent<BoxCollider>();

            float stoneMinX = stoneCollider.bounds.min.x;
            float stoneMaxX = stoneCollider.bounds.max.x;
            float stoneMinZ = stoneCollider.bounds.min.z;
            float stoneMaxZ = stoneCollider.bounds.max.z;

            // calc x and z are overlapping
            float overlapX = Mathf.Max(0, Mathf.Min(thisMaxX, stoneMaxX) - Mathf.Max(thisMinX, stoneMinX));
            float overlapZ = Mathf.Max(0, Mathf.Min(thisMaxZ, stoneMaxZ) - Mathf.Max(thisMinZ, stoneMinZ));

            // if overlapping > 50%, then transform log to this gameObject position
            float overlapPercentX = overlapX / (stoneMaxX - stoneMinX);
            float overlapPercentZ = overlapZ / (stoneMaxZ - stoneMinZ);
            float yDifference = Mathf.Abs(transform.position.y - stone.transform.position.y);

            

            if (overlapPercentX >= 0.5f && overlapPercentZ >= 0.5f && yDifference <= 0.1f && !isTriggered)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                stone.transform.position = newPos;
                if (!liftPlatform.playerEnter) return;

                isTriggered = true;
                goUp = true;
                goDown = false;
                stoneRb.isKinematic = true;
                stoneRb.useGravity = false;
                triggeredObject.SetActive(true);
            }
            else if (isTriggered && (overlapPercentX < 0.5f || overlapPercentZ < 0.5f) && yDifference <= 0.1f)
            {
                isTriggered = false;
                goUp = false;
                goDown = true;
                stoneRb.isKinematic = true;
                stoneRb.useGravity = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (stone == null) return;
        Vector3 pos = stone.transform.position;
        if (goUp)
        {
            if (pos.y >= maxHeight)
            {
                liftPlatform.EnableMovingPlatformScrip();
                goUp = false;
                stoneRb.isKinematic = false;
                stoneRb.useGravity = true;
                transform.position = new Vector3(transform.position.x, stone.transform.position.y, transform.position.z);
            }
            else
            {
                pos.y = Mathf.MoveTowards(pos.y, maxHeight, animateSpeed * Time.deltaTime);
                stone.transform.position = pos;

                pos = transform.position;
                pos.y = Mathf.MoveTowards(pos.y, maxHeight, animateSpeed * Time.deltaTime);
                transform.position = pos;
            }
               
        }
        else if (goDown)
        {
            if (pos.y <= minHeight)
            {
                liftPlatform.InactiveColliders();
                goDown = false;
                stoneRb.isKinematic = false;
                stoneRb.useGravity = true;
                stoneRb.constraints = RigidbodyConstraints.FreezeRotation;
                transform.position = new Vector3(transform.position.x, stone.transform.position.y, transform.position.z);
            }
            else
            {
                pos.y = Mathf.MoveTowards(pos.y, minHeight, animateSpeed * Time.deltaTime);
                stone.transform.position = pos;

                pos = transform.position;
                pos.y = Mathf.MoveTowards(pos.y, minHeight, animateSpeed * Time.deltaTime);
                transform.position = pos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            stone = other.gameObject;
            stoneRb = stone.GetComponent<Rigidbody>();
        }
    }
}
