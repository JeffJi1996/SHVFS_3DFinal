using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> BGMClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (EnemyClose.instance.isEnemyClose == false)
        {
            PlayMoveClip("BGM_Normal");
        }
        else if (EnemyClose.instance.isEnemyClose == true)
        {
            PlayMoveClip("BGM_EnemyClose");
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void PlayMoveClip(string soundName)
    {
        audioSource.clip = FindClip(soundName);
    }

    AudioClip FindClip(string soundName)
    {
        foreach (var clip in BGMClips)
        {
            if (clip.name == soundName)
            {
                return clip;
            }
        }
        Debug.Log("Clip Name is not exist");
        return null;
    }
}
