using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance;
        private SceneController controller;

        private bool passTutorial = false;

        private static int respawnCount = 0;
        
        #endregion

        #region Monobehavior methods
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        private void Start()
        {
            controller = SceneController.instance;
            
            if (!PlayerPrefs.HasKey("PassTutorial"))
            {
                PlayerPrefs.SetInt("PassTutorial", 0);
            }
            else
            {
                int pass = PlayerPrefs.GetInt("PassTutorial");

                if (pass == 0)
                {
                    passTutorial = false;
                }
                else
                {
                    passTutorial = true;
                }
            }
        }

        #endregion
        #region Public methods

        public void AddRespawnCount()
        {
            if (respawnCount < 9999) respawnCount++;
        }

        public int GetRespawnCount()
        {
            return respawnCount;
        }

        public void RestartLevel(string scene)
        {
            LockCursor();
            controller.LoadScene(scene);
        }

        public void LoadMainMenu()
        {
            UnlockCursor();
            controller.LoadScene("MainMenu");
        }

        public void StartGame()
        {
            if (passTutorial)
            {
                LockCursor();
                controller.LoadScene("Level1");
            }
            else
            {
                controller.LoadScene("Tutorial");
            }
        }

        public void ExitGame()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) return;
            Application.Quit();
        }

        public void PassTutorial()
        {
            passTutorial = true;
            if (PlayerPrefs.HasKey("PassTutorial") && PlayerPrefs.GetInt("PassTutorial") == 0)
            {
                PlayerPrefs.SetInt("PassTutorial", 1);
            }
            PlayerPrefs.Save();
        }

        public void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        #endregion
        #region Private methods
        #endregion
    }
}
