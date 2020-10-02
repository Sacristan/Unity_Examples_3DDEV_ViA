using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public void UpdateMusicVolume(float t) => UpdateVolume("MusicVolume", t);
    public void UpdateSFXVolume(float t) => UpdateVolume("SFXVolume", t);

    private void UpdateVolume(string key, float perc)
    {
        if (mixer.GetFloat(key, out float x)) mixer.SetFloat(key, GetVolume(perc));
        else Debug.LogWarningFormat("WARNING: Mixer {0} missing key: {1}", mixer.name, key);
    }

    public static float GetVolume(float rawPerc) //Volume DBs is an exponential value - one needs to use Log here
    {
        float perc = Mathf.Lerp(0.001f, 1f, rawPerc); //To avoid passing 0 to log function
        return Mathf.Log(perc) * 20f;
    }
}
