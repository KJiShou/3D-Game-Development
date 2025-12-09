using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sound;
using Game;
using StarterAssets;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public static UIManager instance;
        private SoundManager soundManager;
        private GameManager gameManager;
        private Scene currentScene;

        [Header("Main Menu Components")]
        [Tooltip("This is only for main menu")]
        public GameObject exitPanel;
        private Animator exitPanelAnim;

        public GameObject settingsPanel;
        private Animator settingsPanelAnim;

        public GameObject soundBtnObj;
        public Sprite soundBtnSpirte;
        public Sprite pressedSoundBtnSpirte;
        public Sprite mutedSoundBtnSpirte;
        public Sprite pressedMutedSoundBtnSpirte;
        private Image soundBtnImage;
        private Button soundBtn;

        [Header("Levels Components")]
        [Tooltip("This is for the level scenes")]
        public GameObject pausePanel;
        private Animator pausePanelAnim;
        private bool pausePanelIsOpen = false;

        public GameObject infoPanel;
        private Animator infoPanelAnim;
        private bool infoPanelIsOpen = false;

        public GameObject player;
        private ThirdPersonController thirdPersonController;


        #endregion

        #region Monobehavior methods

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            soundManager = SoundManager.instance;
            gameManager = GameManager.instance;
            currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "MainMenu")
            {
                soundBtnImage = soundBtnObj.GetComponent<Image>();
                soundBtn = soundBtnObj.GetComponent<Button>();
                settingsPanelAnim = settingsPanel.GetComponent<Animator>();
                exitPanelAnim = exitPanel.GetComponent<Animator>();
            }
            else
            {
                pausePanelAnim = pausePanel.GetComponent<Animator>();
                thirdPersonController = player.GetComponentInChildren<ThirdPersonController>();
                infoPanelAnim = infoPanel.GetComponent<Animator>();
            }
        }

        private void Update()
        {
            if(currentScene.name != "MainMenu")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pausePanelIsOpen = !pausePanelIsOpen;
                    if (pausePanelIsOpen)
                    {
                        gameManager.UnlockCursor();
                        pausePanel.SetActive(true);
                        thirdPersonController.enabled = false;
                    }
                    else
                    {
                        if(infoPanelIsOpen)
                        {
                            CloseInfoPanel();
                        }
                        else
                        {
                            PausePanelClose();
                        }
                    }
                }
            }
        }
        #endregion

        #region Public methods

        public void OpenInfoPanel()
        {
            infoPanelIsOpen = true;
            infoPanel.SetActive(true);
        }

        public void CloseInfoPanel()
        {
            infoPanelIsOpen = false;
            infoPanelAnim.SetTrigger("close");
            StartCoroutine(WaitInfoPanelAnim());
        }

        public void RestartLevel()
        {
            gameManager.RestartLevel(currentScene.name);
        }

        public void ExitGame()
        {
            gameManager.ExitGame();
        }

        public void StartGame()
        {
            gameManager.StartGame();
        }

        public void BackToMainMenu()
        {
            gameManager.LoadMainMenu();
        }

        public void PausePanelClose()
        {
            pausePanelIsOpen = false;
            gameManager.LockCursor();
            pausePanelAnim.SetTrigger("close");
            StartCoroutine(ClosePausePanel());
            thirdPersonController.enabled = true;
        }

        public void SettingsClicked()
        {
            settingsPanel.SetActive(true);
        }

        public void SettingsPanelClose()
        {
            settingsPanelAnim.SetTrigger("close");
            StartCoroutine(CloseSettingsPanel());
        }

        public void ExitGameClicked()
        {
            exitPanel.SetActive(true);
        }

        public void CloseExitGamePanel()
        {
            exitPanelAnim.SetTrigger("close");
            StartCoroutine(CloseExitPanel());
        }

        public void ChangeToSoundSprite()
        {
            soundBtnImage.sprite = soundBtnSpirte;
            SpriteState state = soundBtn.spriteState;
            state.pressedSprite = pressedSoundBtnSpirte;
            soundBtn.spriteState = state;
        }

        public void ChangeToMutedSoundSprite()
        {
            soundBtnImage.sprite = mutedSoundBtnSpirte;
            SpriteState state = soundBtn.spriteState;
            state.pressedSprite = pressedMutedSoundBtnSpirte;
            soundBtn.spriteState = state;
        }

        #endregion

        #region Private methods

        private IEnumerator CloseSettingsPanel()
        {
            yield return new WaitForSeconds(0.4f);
            settingsPanel.SetActive(false);
        }

        private IEnumerator CloseExitPanel()
        {
            yield return new WaitForSeconds(0.4f);
            exitPanel.SetActive(false);
        }

        private IEnumerator ClosePausePanel()
        {
            yield return new WaitForSeconds(0.4f);
            pausePanel.SetActive(false);
        }

        private IEnumerator WaitInfoPanelAnim()
        {
            yield return new WaitForSeconds(0.4f);
            infoPanel.SetActive(false);
        }

        #endregion
    }
}
