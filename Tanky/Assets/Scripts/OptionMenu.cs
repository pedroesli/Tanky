using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    private Resolution[] resolutions;

    void Start()
    {
        SetupResollutionDropdown();

        SetupVolumeSlider();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFilter(int filterIndex)
    {
        Camera2DFilters camera2DFilter = LoadCameraFilter();
        if(camera2DFilter != null)
        {
            GameManager.instance.filterIndex = filterIndex;
            camera2DFilter.Filter = Camera2DFilters._2DFilter.CRTAperture + filterIndex;
            camera2DFilter.Start();
        }
    }

    public Camera2DFilters LoadCameraFilter()
    {
        Camera2DFilters camera2DFilter = GameObject.Find("Main Camera").GetComponent<Camera2DFilters>();
        if (camera2DFilter == null)
        {
            Debug.LogWarning("Camera2DFilters script is missing on the cammera!");
            return null;
        }
        else
            return camera2DFilter;
    }
    private void SetupResollutionDropdown()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    private void SetupVolumeSlider()
    {
        float masterVolume;
        MusicManager.instance.audioMixer.GetFloat("MasterVolume", out masterVolume);
        print(masterVolume);
        volumeSlider.value = masterVolume;
    }
}
