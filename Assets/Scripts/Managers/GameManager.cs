using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance;
        private SceneController controller;
        private bool kakaIsSave = false;
        private bool wawaIsSave = false;
        private bool passTutorial = false;
        private static int lifeLeft = 5;
        private static int currentLevel = 0;
        
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
            if (!PlayerPrefs.HasKey("KakaIsSaved"))
            {
                PlayerPrefs.SetInt("KakaIsSaved", 0);
            }
            else
            {
                kakaIsSave = PlayerPrefs.GetInt("KakaIsSaved").Equals(1);
            }

            if (!PlayerPrefs.HasKey("WawaIsSaved"))
            {
                PlayerPrefs.SetInt("WawaIsSaved", 0);
            }
            else
            {
                wawaIsSave = PlayerPrefs.GetInt("WawaIsSaved").Equals(1);
            }

            if (!PlayerPrefs.HasKey("CurrentLevel"))
            {
                PlayerPrefs.SetInt("CurrentLevel", 1);
            }
            else
            {
                currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            }
        }

        #endregion
        #region Public methods

        public void DeductLife()
        {
            lifeLeft--;

            if(lifeLeft <= 0)
            {
                ElectricCollect.charge = 0;
                LoadLoseScene();
            }
        }

        public void ResetLeftLife()
        {
            lifeLeft = 5;
        }

        public int GetLeftLife()
        {
            return lifeLeft;
        }

        public void RestartLevel(string scene)
        {
            ResetLeftLife();
            LockCursor();
            controller.LoadScene(scene);
        }

        public void LoadMainMenu()
        {
            ResetLeftLife();
            UnlockCursor();
            controller.LoadScene("MainMenu");
        }

        public void LoadLoseScene()
        {
            ResetLeftLife();
            UnlockCursor();
            //controller.LoadScene("loseScene");
        }

        public void StartGame()
        {
            if (passTutorial)
            {
                LockCursor();
                switch (currentLevel){
                    case 1:
                        {
                            controller.LoadScene("Level1");
                            break;
                        }
                    case 2:
                        {
                            controller.LoadScene("Level2");
                            break;
                        }
                    case 3:
                        {
                            controller.LoadScene("Level3");
                            break;
                        }
                    default:
                        controller.LoadScene("Level1");
                        break;
                }
                
            }
            else
            {
                controller.LoadScene("Tutorial");
            }
        }

        public void PlayCutScene()
        {
            LockCursor();
            controller.LoadScene("Introduce");
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

        public bool IsKakaSave()
        {
            return kakaIsSave;
        }

        public bool IsWawaSave()
        {
            return wawaIsSave;
        }

        public void SaveKaka()
        {
            kakaIsSave = true;
            PlayerPrefs.SetInt("KakaIsSaved", 1);
        }

        public void SaveWawa()
        {
            wawaIsSave = true;
            PlayerPrefs.SetInt("WawaIsSaved", 1);
        }

        public void UpdateCurrentLevel()
        {
            currentLevel++;
            if (currentLevel > 3) currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.Save();
        }

        #endregion
        #region Private methods
        #endregion
    }
}
