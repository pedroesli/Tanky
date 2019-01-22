using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    [HideInInspector]
    public int filterIndex;

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
        filterIndex = 1;
    }

    private void OnLevelWasLoaded(int level)
    {
        print(level);
        if (level == 1)
            MusicManager.instance.Play("Mechanical_noise");
        ApplySettings();
    }

    private void ApplySettings()
    {
        OptionMenu optionMenu = new OptionMenu();
        optionMenu.SetFilter(filterIndex);
    }
}
