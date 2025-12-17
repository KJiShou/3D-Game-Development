using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sound;
using Game;
using StarterAssets;
using TMPro;

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

        [Header("Tutorial Components")]
        [Tooltip("This is only for the tutorial scene")]
        public GameObject firstGuide;
        public GameObject warningMsg;
        private Animator warningMsgAnim;

        [Header("In game Components")]
        [Tooltip("This is for the level scenes")]
        public GameObject pausePanel;
        private Animator pausePanelAnim;
        private bool pausePanelIsOpen = false;

        public GameObject infoPanel;
        private Animator infoPanelAnim;
        private bool infoPanelIsOpen = false;

        public GameObject player;
        private ThirdPersonController thirdPersonController;

        public TextMeshProUGUI respawnCount;

        [Header("Level 2 Components")]
        [Tooltip("This is only for the level 2 scenes")]
        public GameObject[] thunderUI;
        public Material thunderUIMat;
        private ElectricCollect electricCollect;

        [Header("Key UI")]
        public GameObject keyIcon;
        private KeyCollect keyCollect;

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
                
                if (thirdPersonController != null && currentScene.name == "Tutorial")
                {
                    gameManager.UnlockCursor();
                    thirdPersonController.enabled = false;
                    warningMsgAnim = warningMsg.GetComponent<Animator>();
                }
                else if (currentScene.name == "Level2")
                {
                    electricCollect = ElectricCollect.instance;
                    keyCollect = KeyCollect.instance;
                }
                respawnCount.text = "X " + gameManager.GetLeftLife();
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

        // ==============================
        // In Game methods
        // ==============================
        public void UpdateLeftLife()
        {
            respawnCount.text = "X " + gameManager.GetLeftLife();
        }

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
            StartCoroutine(WaitBtnAnim());
            gameManager.PlayCutScene();
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
            StartCoroutine(WaitBtnAnim());
            settingsPanel.SetActive(true);
        }

        public void SettingsPanelClose()
        {
            settingsPanelAnim.SetTrigger("close");
            StartCoroutine(CloseSettingsPanel());
        }

        public void ExitGameClicked()
        {
            StartCoroutine(WaitBtnAnim());
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

        public void CloseFirstGuidePanel()
        {
            gameManager.LockCursor();
            firstGuide.SetActive(false);
            thirdPersonController.enabled = true;
            warningMsg.SetActive(true);
            StartCoroutine(WaitWarningMsg());
        }

        // ==============================
        // Level 2 methods
        // ==============================
        public void UpdateThunderCount()
        {
            if (!thunderUIMat) return;

            if (electricCollect.GetCharge() > 4) return;

            RawImage image = thunderUI[electricCollect.GetCharge() - 1].gameObject.GetComponent<RawImage>();
            image.color = Color.white;
            image.material = thunderUIMat;
        }

        public void UpdateKeyUI()
        {
            RawImage image = keyIcon.gameObject.GetComponent<RawImage>();
            image.color = Color.white;
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

        private IEnumerator WaitWarningMsg()
        {
            yield return new WaitForSeconds(3f);
            warningMsgAnim.SetTrigger("close");
            yield return new WaitForSeconds(0.4f);
            warningMsg.SetActive(false);
        }

        private IEnumerator WaitBtnAnim()
        {
            yield return new WaitForSeconds(0.5f);
        }
        #endregion
    }
}
