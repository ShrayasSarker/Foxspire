using UnityEngine;
using UnityEngine.Audio; // For AudioMixer

public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer; // Assign via Inspector

    // Call this to set music volume (slider value between 0 and 1)
    public void SetMusicVolume(float volume)
    {
        // Convert linear volume to decibels
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    // Same for SFX volume
    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }
}

