using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField]
    public Slider musicSlider;
    public Slider soundSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio("Theme_Song");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ToggleMusic()
    {

        Sound s = Array.Find(sounds, sound => sound.name == "Theme_Song");
        s.source.mute = !s.source.mute;
    }

    public void ToggleSound()
    {
        Sound s = Array.Find(sounds, sound => sound.name != "Theme_Song");
        s.source.mute = !s.source.mute;
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = musicSlider.value;
    }


    public void ChangeSoundVolume()
    {
        AudioListener.volume = soundSlider.value;
    }
}
