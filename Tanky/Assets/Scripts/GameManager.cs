using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public Camera2DFilters._2DFilter filter;
    public Camera2DFilters.OutputResolition outputResolition;
    public static GameManager instance;
    private Camera2DFilters camera2DFilters;
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

        camera2DFilters = GameObject.Find("Main Camera").GetComponent<Camera2DFilters>();
        if (camera2DFilters == null)
            Debug.LogWarning("Camera2DFilters script is missing on the cammera!");

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
    public void ConfigureCamera(Camera2DFilters._2DFilter filter)
    {

    }
}
