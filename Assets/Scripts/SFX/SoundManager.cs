using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour {

    public List<AudioClip> sounds;
    public static SoundManager instance;

    private ObjectPool soundPool;
    private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();

    

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        soundPool = GetComponent<ObjectPool>();

    }

    private void Start()
    {
        foreach (var sound in sounds)
        {
            nameToSound.Add(sound.name, sound);
        }
    }

    public void AddSounds(List<AudioClip> soundsToAdd)
    {
        foreach (var sound in soundsToAdd)
        {
            nameToSound.Add(sound.name, sound);
        }
    }

    public void RemoveSounds(List<AudioClip> soundsToAdd)
    {
        foreach (var sound in soundsToAdd)
        {
            nameToSound.Remove(sound.name);
        }
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        if(clip!=null)
        {
            soundPool.GetObject().GetComponent<SoundFx>().Play(clip, loop);
        }
    }

    public void PlaySound(string soundName, bool loop = false)
    {
        var clip = nameToSound[soundName];
        if (clip != null)
        {
            PlaySound(clip, loop);
        }
    }

   

    public void PlayLoopedSound(string soundName, bool loop = false, float time = 0f)
    {
        var clip = nameToSound[soundName];
        if(clip!=null)
        {
            if(time<= 0f)
            {
                time = clip.length;
            }
            soundPool.GetObject().GetComponent<SoundFx>().Play(clip, loop, time);
        }
    }

    public void StopCurrentClip()
    {
        soundPool.GetObject().GetComponent<SoundFx>().StopCurrentClip();
    }

    public void SetSoundEnable(bool soundEnabled)
    {
        PlayerPrefs.SetInt("sound_enabled", soundEnabled ? 1 : 0);
    }

    public void SetMusicEnable(bool musicEnabled)
    {
        PlayerPrefs.SetInt("music_enabled", musicEnabled ? 1 : 0);
    }

    public void ToggleSound()
    {
        var sound = PlayerPrefs.GetInt("sound_enabled");
        PlayerPrefs.SetInt("sound_enabled", 1 - sound);
    }

    public void ToggleMusic()
    {
        var music = PlayerPrefs.GetInt("music_enabled");
        PlayerPrefs.SetInt("music_enabled", 1 - music);
    }

} 
