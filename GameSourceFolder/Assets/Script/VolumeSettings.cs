//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description:
 * This is to control the volume of background and sound effects. user can do this in options scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider;
    public Slider SFXSlider;


    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("BGM",Mathf.Log10(volume)*20);
        Debug.Log("Music volume set to: " + volume);
    }

    public void SetSFXvolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        Debug.Log("SFX volume set to: " + volume);
    }
}
