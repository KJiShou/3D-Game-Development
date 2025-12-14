using UnityEngine;

public class PumpkinSelector : MonoBehaviour
{
    public enum PumpkinColor { Green, Red, Blue, Purple, Orange }
    public PumpkinColor pumpkinColor;
    public UIPressButton pressButton;
    public StartMessage message;
    public GameObject endPoint;
    public MovingPlatforms movingPlatform;

    public bool isCorrect = false;
    private bool playerInRange = false;
    public GameObject explosionPrefab;

    public void ShowPrompt(bool show)
    {
        if (show)
        {
            pressButton.UpdateTarget(transform);
            pressButton.Show();
        }
        else
            pressButton.Hide();
    }

    public void Select()
    {
        if (isCorrect)
        {
            message.ShowMessage("You are correct ! Now you can use the platform to go up !");
            movingPlatform.endPoint = endPoint.transform;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong Pumpkin!");
            message.ShowMessage("You are wrong ! Please try again ! ");
            CameraShake.Instance.ShakeCamera(2f, 1f, 5.0f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
