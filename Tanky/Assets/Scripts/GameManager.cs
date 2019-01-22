using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public Dropdown resolutionDropdown; 
    private Resolution[] resolutions;
    private Camera2DFilters camera2DFilters;
    private int filterIndex = 1;
    private int pause = 1;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        LoadCameraFilter();

    }
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i< resolutions.Length; i++)
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause++;
            Time.timeScale = pause % 2 ==0 ? 0 : 1;
        }
    }
    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Scene loaded: " + level);
        LoadCameraFilter();
        SetFilter(filterIndex);
    }
    public void PlayGame()
    {
        MusicManager.instance.Play("Menu_select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        MusicManager.instance.Play("Menu_select");
        Application.Quit();
    }
    public void SetActive(GameObject obj)
    {
        MusicManager.instance.Play("Menu_select");
        obj.SetActive(true);
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
        if(camera2DFilters!=null)
        {
            this.filterIndex = filterIndex;
            camera2DFilters.Filter = Camera2DFilters._2DFilter.CRTAperture + filterIndex ;
            camera2DFilters.Start();
        }
    }
    public void LoadCameraFilter()
    {
        camera2DFilters = GameObject.Find("Main Camera").GetComponent<Camera2DFilters>();
        if (camera2DFilters == null)
            Debug.LogWarning("Camera2DFilters script is missing on the cammera!");
    }
}
