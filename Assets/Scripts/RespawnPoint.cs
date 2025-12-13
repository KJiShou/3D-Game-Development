using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private CandleController candle;
    public bool isActive = false;

    private void Start()
    {
        candle = GetComponent<CandleController>();
        candle.TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RespawnManager.instance.SetRespawnPoint(this);
    }

    public void Activate()
    {
        isActive = true;
        candle.TurnOn();
    }

    public void Deactivate()
    {
        isActive = false;
        candle.TurnOff();
    }
}