using UnityEngine;

public class CandleController : MonoBehaviour
{
    public GameObject candleFlame1;
    public GameObject candleFlame2;
    public GameObject candleFlame3;
    public GameObject light;

    public void TurnOn()
    {
        candleFlame1.SetActive(true);
        candleFlame2.SetActive(true);
        candleFlame3.SetActive(true);
        light.SetActive(true);
    }

    public void TurnOff()
    {
        candleFlame1.SetActive(false);
        candleFlame2.SetActive(false);
        candleFlame3.SetActive(false);
        light.SetActive(false);
    }
}
