using UnityEngine;
using UnityEngine.UI;

public class ElectricCollectUI : MonoBehaviour
{
    public static int charge = 0;
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
        charge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(charge == 1)
        {
            img.texture = charge1tex;
            img.enabled = true;
            
        }
        else if(charge == 2)
        {
            img.texture = charge2tex;
        }
        else if(charge == 3)
        {
            img.texture = charge3tex;
        }
        else if(charge >= 4)
        {
            img.texture = charge4tex;
        }
        else
        {
            img.texture = charge0tex;
        }
    }

    
}
