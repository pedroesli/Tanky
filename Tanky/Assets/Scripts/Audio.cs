using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(-3f,3f)]
    public float pitch;
    [Range(0f,1f)]
    public float spatialBlend;
    
    [HideInInspector]
    public AudioSource source;

    public void Configure(GameObject musicManager)
    {
        source = musicManager.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.spatialBlend = spatialBlend;
    }
}
