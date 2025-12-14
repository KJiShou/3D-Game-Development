using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    private RawImage img;

    void Start()
    {
        img = GetComponent<RawImage>();
        img.enabled = false;
    }

    void Update()
    {
        img.enabled = KeyCollect.hasKey;
    }
}
