using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        #region Variables
        // manager intances
        public static SoundManager instance;
        private UIManager uiManager;

        private bool isMuted = false;

        [Header("Volume Settings")]
        public Slider masterVol;
        public Slider sfxVol;
        public Slider musicVol;
        public AudioMixer mainAudioMixer;

        private Scene currentScene;
        #endregion

        #region MonoBehaviour methods

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            currentScene = SceneManager.GetActiveScene();
            uiManager = UIManager.instance;

            if (!PlayerPrefs.HasKey("MasterVol"))
            {
                PlayerPrefs.SetFloat("MasterVol", 0.0f);
            }
            if (!PlayerPrefs.HasKey("SFXVol"))
            {
                PlayerPrefs.SetFloat("SFXVol", 0.0f);
            }
            if (!PlayerPrefs.HasKey("MusicVol"))
            {
                PlayerPrefs.SetFloat("MusicVol", 0.0f);
            }

            float vol;
            if (PlayerPrefs.HasKey("MasterVol"))
            {
                vol = PlayerPrefs.GetFloat("MasterVol");
                if (masterVol)
                {
                    masterVol.value = vol;
                    SetMasterVolume(vol);
                }
            }
            if (PlayerPrefs.HasKey("SFXVol"))
            {
                vol = PlayerPrefs.GetFloat("SFXVol");
                if (sfxVol)
                {
                    sfxVol.value = vol;
                    SetSFXVolume(vol);
                }
            }
            if (PlayerPrefs.HasKey("MusicVol"))
            {
                vol = PlayerPrefs.GetFloat("MusicVol");
                if (musicVol)
                {
                    musicVol.value = vol;
                    SetMusicVolume(vol);
                }
            }
        }
        #endregion

        #region Public methods

        public void MuteSound()
        {
            isMuted = !isMuted;
            if (isMuted)
            {
                uiManager.ChangeToMutedSoundSprite();
                float muteVol = -80.0f;
                masterVol.value = muteVol;
                sfxVol.value = muteVol;
                musicVol.value = muteVol;
            }
            else
            {
                float vol = 0.0f;
                uiManager.ChangeToSoundSprite();
                masterVol.value = vol;
                sfxVol.value = vol;
                musicVol.value = vol;
            }

        }

        public void ChangeMasterVolume()
        {
            CheckAllVolumeIsMuted();

            SetMasterVolume(masterVol.value);
            if (PlayerPrefs.HasKey("MasterVol"))
            {
                PlayerPrefs.SetFloat("MasterVol", masterVol.value);
            }
        }

        public void ChangeSFXVolume()
        {
            CheckAllVolumeIsMuted();

            SetSFXVolume(sfxVol.value);
            if (PlayerPrefs.HasKey("SFXVol"))
            {
                PlayerPrefs.SetFloat("SFXVol", sfxVol.value);
            }

        }
        public void ChangeMusicVolume()
        {
            CheckAllVolumeIsMuted();

            SetMusicVolume(musicVol.value);
            if (PlayerPrefs.HasKey("MusicVol"))
            {
                PlayerPrefs.SetFloat("MusicVol", musicVol.value);
            }
        }

        #endregion

        #region Private methods

        private void SetMasterVolume(float volume)
        {
            mainAudioMixer.SetFloat("Master", volume);
        }

        private void SetMusicVolume(float volume)
        {
            mainAudioMixer.SetFloat("Music", volume);
        }

        private void SetSFXVolume(float volume)
        {
            mainAudioMixer.SetFloat("SFX", volume);
        }

        private void CheckAllVolumeIsMuted()
        {
            if (currentScene.name != "MainMenu") return;
            
            if (masterVol.value <= -80.0f || (sfxVol.value <= -80.0f && musicVol.value <= -80.0f))
            {
                isMuted = true;
                uiManager.ChangeToMutedSoundSprite();
            }
            else
            {
                isMuted = false;
                uiManager.ChangeToSoundSprite();
            }
        }
        #endregion
    }
}
