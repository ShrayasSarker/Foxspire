using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;// The audio clip to play
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;// The audio source to play the sound on
}
