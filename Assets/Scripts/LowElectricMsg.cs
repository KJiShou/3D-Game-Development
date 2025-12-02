using UnityEngine;
using System.Collections;
using TMPro;

public class LowElectricMsg : MonoBehaviour
{
    public static LowElectricMsg instance;

    private TMP_Text text;
    private Coroutine showRoutine;

    private void Awake()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
        SetAlpha(0);
    }

    public void Show(string msg, float duration = 2f)
    {
        if (showRoutine != null)
            StopCoroutine(showRoutine);

        showRoutine = StartCoroutine(ShowRoutine(msg, duration));
    }

    private IEnumerator ShowRoutine(string msg, float duration)
    {
        text.text = msg;

        // fade in
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            SetAlpha(t / 0.2f);
            yield return null;
        }
        SetAlpha(1);

        yield return new WaitForSeconds(duration);

        // fade out
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            SetAlpha(1 - t / 0.3f);
            yield return null;
        }
        SetAlpha(0);
    }

    private void SetAlpha(float a)
    {
        Color c = text.color;
        c.a = a;
        text.color = c;
    }
}
