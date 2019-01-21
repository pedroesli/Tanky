using System.Collections;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Audio[] audios;
    public static MusicManager instance;
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
        foreach(Audio a in audios)
        {
            a.Configure(gameObject);
        }
    }
    void Start()
    {
        
    }

    public void Play(string name)
    {
        Audio sound = Array.Find(audios, audio => audio.name == name);
        if (sound != null)
            sound.source.Play();
        else
            Debug.LogWarning("Did not find " + name + " in audio array");
    }
}
