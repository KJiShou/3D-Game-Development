using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public GameObject colliders;
    public Transform targetPos;
    private float speed = 3f;

    public GameObject halfTransparentStoneObj;
    private HalfTransparentStone halfTransparentStone;

    public MovingPlatform[] childMovingPlatformScripts;

    private void Start()
    {
        halfTransparentStone = halfTransparentStoneObj.GetComponent<HalfTransparentStone>();
    }

    private void LateUpdate()
    {
        if (halfTransparentStone.goUp)
        {
            DisableMovingPlatformScrip();
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, targetPos.position.y, speed * Time.deltaTime);
            transform.position = pos;
        }
        else if (halfTransparentStone.goDown)
        {
            DisableMovingPlatformScrip();
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, -4.2f, speed * Time.deltaTime);
            transform.position = pos;
        }
    }

    public void EnableMovingPlatformScrip()
    {
        colliders.SetActive(true);
        foreach (MovingPlatform mp in childMovingPlatformScripts)
        {
            mp.enabled = true;
        }
    }

    public void DisableMovingPlatformScrip()
    {
        colliders.SetActive(true);
        foreach (MovingPlatform mp in childMovingPlatformScripts)
        {
            mp.enabled = false;
        }
    }

    public void InactiveColliders()
    {
        colliders.SetActive(false);
    }
}
