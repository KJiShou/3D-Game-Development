using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Resolution
{
    public class ResolutionManager : MonoBehaviour
    {
        #region Variables
        public static ResolutionManager instance;

        public TMP_Dropdown resolution;
        #endregion

        #region Monobehavior methods
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            bool isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
            if (isWebGL)
            {
                // if web platform, then not allow to modify resolution
                resolution.ClearOptions();
                resolution.AddOptions(new System.Collections.Generic.List<string> { "-" });
                resolution.interactable = false;
            }
            else
            {
                if (!PlayerPrefs.HasKey("Resolution"))
                {
                    PlayerPrefs.SetInt("Resolution", 0);
                    Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
                }
                else
                {
                    resolution.value = PlayerPrefs.GetInt("Resolution");
                }
            }
        }
        #endregion

        #region Public methods
        public void SetResolution()
        {
            int index = resolution.value;
            if (PlayerPrefs.HasKey("Resolution")) PlayerPrefs.SetInt("Resolution", index);
            PlayerPrefs.Save();
            switch (index)
            {
                case 0: // Fullscreen native
                    Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
                    break;
                case 1:
                    ApplyWindowedResolution(1366, 768);
                    break;
                case 2:
                    ApplyWindowedResolution(1920, 1080);
                    break;
                case 3:
                    ApplyWindowedResolution(2560, 1440);
                    break;
            }
        }

        #endregion

        #region Private methods
        private void ApplyWindowedResolution(int w, int h)
        {
            // if player's screen smaller than selected resolution, then fallback to its max screen size
            if (w > Display.main.systemWidth || h > Display.main.systemHeight)
            {
                w = Display.main.systemWidth;
                h = Display.main.systemHeight;
            }

            Screen.SetResolution(w, h, FullScreenMode.Windowed);
        }

        #endregion
    }
}