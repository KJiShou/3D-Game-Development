using UnityEngine;

public class CandleController : MonoBehaviour
{
    public ParticleSystem candleFlame1;
    public ParticleSystem candleFlame2;
    public ParticleSystem candleFlame3;
    public GameObject candleLight;

    public void TurnOn()
    {
        candleFlame1.Play();
        candleFlame2.Play();
        candleFlame3.Play();
        candleLight.SetActive(true);
    }

    public void TurnOff()
    {
        candleFlame1.Stop();
        candleFlame2.Stop();
        candleFlame3.Stop();
        candleLight.SetActive(false);
    }
}
