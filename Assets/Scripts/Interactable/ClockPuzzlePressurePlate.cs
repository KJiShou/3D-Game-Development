using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ClockPuzzlePressurePlate : MonoBehaviour
{
    private ClockPuzzle clockPuzzle;
    private string triggerTag = "Triggered Stone";
    private int selfChildIndex = 0;
    private Material mat;

    private bool isTriggered = false;
    private bool isSuccess = false;
    private bool isFail = false;

    private float maxIntensity = 1f;
    private float pulseSpeed = 1f;

    private float intensity = 1f;

    private void Start()
    {
        clockPuzzle = ClockPuzzle.instance;
        selfChildIndex = transform.GetSiblingIndex();
        Renderer r = GetComponentInChildren<Renderer>();
        mat = r.material;
        mat.DisableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.black);
    }

    //private void Update()
    //{
    //    //if (isSuccess)
    //    //{
    //    //    OnPuzzleSuccess();
    //    //}

    //    //if (isFail)
    //    //{
    //    //    OnPuzzleFail();
    //    //}
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(triggerTag) && !isTriggered && !isSuccess)
        {
            isTriggered = true;
            TriggeredPressurePlate();
            clockPuzzle.CompareTriggerNumber(selfChildIndex);
        }
    }

    public void TriggeredPressurePlate()
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.yellow * intensity);
    }

    public void OnPuzzleSuccess()
    {
        isSuccess = true;
        mat.EnableKeyword("_EMISSION");
        // float intensity = Mathf.PingPong(Time.time * pulseSpeed, maxIntensity);
        mat.SetColor("_EmissionColor", Color.green * intensity);
    }

    public void OnPuzzleFail()
    {
        isFail = true;
        mat.EnableKeyword("_EMISSION");
        // float intensity = Mathf.PingPong(Time.time * pulseSpeed, maxIntensity);
        mat.SetColor("_EmissionColor", Color.red * intensity);
    }

    public IEnumerator ResetTriggered()
    {
        yield return new WaitForSeconds(3.0f);
        isTriggered = false;
        isFail = false;
        mat.DisableKeyword("_EMISSION");
    }
}
