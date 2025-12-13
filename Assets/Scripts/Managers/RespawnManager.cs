using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    private Scene currentScene;
    public RespawnPoint currentRespawn;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        currentScene = SceneManager.GetActiveScene();
    }

    public void SetRespawnPoint(RespawnPoint newPoint)
    {
        if (currentRespawn != null)
            currentRespawn.Deactivate();

        currentRespawn = newPoint;
        currentRespawn.Activate();
    }

    public void RespawnPlayer(GameObject player)
    {
        if (currentRespawn == null)
        { 
            return;
        }

        player.transform.position = currentRespawn.transform.position;
    }
}
