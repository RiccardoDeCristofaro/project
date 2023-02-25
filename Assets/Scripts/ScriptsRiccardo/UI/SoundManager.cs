using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
            Load();
    }

    public void ChangedVolume()
    {
        AudioListener.volume= volumeSlider.value;
        Save(); 
    }
    public void Load()
    {
        volumeSlider.value= PlayerPrefs.GetFloat("musicValue");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicValue", volumeSlider.value);
    }
}
