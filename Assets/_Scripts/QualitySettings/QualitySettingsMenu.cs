using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class QualitySettingsMenu : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown; // Attach a TMP_Dropdown UI element

    void Start()
    {
        // Populate the dropdown with quality levels
        string[] qualityLevels = QualitySettings.names;
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(qualityLevels));

        // Set the current quality level as the dropdown value
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // Add listener for when the dropdown value changes
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    // Change quality level
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
}
