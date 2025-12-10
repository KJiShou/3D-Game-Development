using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElectricCollectUI : MonoBehaviour
{
    public Texture charge1tex;
    public Texture charge2tex;
    public Texture charge3tex;
    public Texture charge4tex;
    public Texture charge0tex;
    private RawImage img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<RawImage>();
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int currentCharge = ElectricCollect.charge;

        if(currentCharge == 1)
        {
            img.texture = charge1tex;
            img.enabled = true;
            
        }
        else if(currentCharge == 2)
        {
            img.texture = charge2tex;
        }
        else if(currentCharge == 3)
        {
            img.texture = charge3tex;
        }
        else if(currentCharge >= 4)
        {
            img.texture = charge4tex;
        }
        else
        {
            img.texture = charge0tex;
        }

        if (currentCharge == 0 && img.enabled)
        {
             img.enabled = false; // 如果 charge 为 0 时应该隐藏 UI
             img.texture = charge0tex;
        }
    }

    
}
