using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [HideInInspector]
    public int filterIndex = 1;

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
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            MusicManager.instance.Play("Mechanical_noise");
        }
            
        ApplySettings();
        
    }

    private void ApplySettings()
    {
        OptionMenu optionMenu = new OptionMenu();
        optionMenu.SetFilter(filterIndex);
    }
}
