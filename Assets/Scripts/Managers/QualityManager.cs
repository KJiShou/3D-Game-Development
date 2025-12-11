using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QualityManager : MonoBehaviour
{
    [Header("UI Dropdown for Quality Settings")]
    public TMP_Dropdown qualityDropdown;

    private void Start()
    {
        bool isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
        if (isWebGL)
        {
            // if web platform, then not allow to modify quality
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(new System.Collections.Generic.List<string> { "-" });
            qualityDropdown.interactable = false;

            // WebGL quality level is 5
            QualitySettings.SetQualityLevel(4, true);
        }
        else
        {
            if (!PlayerPrefs.HasKey("QualityLevel"))
            {
                // default is high quality
                PlayerPrefs.SetInt("QualityLevel", 2);
                qualityDropdown.value = 2;
                QualitySettings.SetQualityLevel(2, true);
            }
            else
            {
                int value = PlayerPrefs.GetInt("QualityLevel");
                qualityDropdown.value = value;
                QualitySettings.SetQualityLevel(value, true);
            }
            qualityDropdown.onValueChanged.AddListener(SetQuality);
        }
    }

    /// <summary>
    /// Toggle quality
    /// </summary>
    /// <param name="index">0=Low, 1=Medium, 2=High, 3=Very high</param>
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
        PlayerPrefs.SetInt("QualityLevel", qualityDropdown.value);
        PlayerPrefs.Save();
    }

}
