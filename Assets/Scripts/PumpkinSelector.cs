using CurvedPathGenerator;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PumpkinSelector : MonoBehaviour
{
    public enum PumpkinColor { Green, Red, Blue, Purple, Orange }
    public PumpkinColor pumpkinColor;
    public UIPressButton pressButton;
    public StartMessage message;
    public GameObject endPoint;
    public MovingPlatforms movingPlatform;
    public GameObject successFollower;

    public bool isCorrect = false;
    private bool playerInRange = false;
    public GameObject explosionPrefab;
    public GameObject successPartical;
    private PathFollower pathFollower;

    private void Start()
    {
        if (successFollower != null) pathFollower = successFollower.GetComponent<PathFollower>();
    }

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
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            if (successFollower != null) successFollower.SetActive(true);
            if (pathFollower != null) pathFollower.StartFollow();
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
