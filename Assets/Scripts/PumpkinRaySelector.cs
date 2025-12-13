using UnityEngine;

public class PumpkinRaySelector : MonoBehaviour
{
    public float rayDistance = 4f;
    public LayerMask pumpkinMask;

    private PumpkinSelector currentPumpkin = null;

    void Update()
    {
        CheckRaycast();

        if (currentPumpkin != null && Input.GetKeyDown(KeyCode.E))
        {
            currentPumpkin.Select();
        }
    }

    void CheckRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, pumpkinMask))
        {
            PumpkinSelector pumpkin = hit.collider.GetComponent<PumpkinSelector>();

            if (pumpkin != null)
            {
                if (currentPumpkin != pumpkin)
                {
                    currentPumpkin = pumpkin;
                    pumpkin.ShowPrompt(true);
                }
                return;
            }
        }

        if (currentPumpkin != null)
        {
            currentPumpkin.ShowPrompt(false);
            currentPumpkin = null;
        }
    }
}
