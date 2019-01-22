using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
    public Audio[] audios;
    public static MusicManager instance;
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;
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
            a.Configure(gameObject.AddComponent<AudioSource>());
            a.source.outputAudioMixerGroup = audioMixerGroup;
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
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
