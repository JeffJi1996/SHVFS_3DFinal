using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SoundFx : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsTrue(audioSource != null);
    }

    public void Play(AudioClip clip, bool loop=false)
    {
        if(clip==null)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        if(!loop)
        {
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }
    }

    public void Play(AudioClip clip,bool loop = false,float time = 0f)
    {
        if(clip=null)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        if(!loop)
        {
            Invoke("DisableSoundFx", clip.length + 0.1f);
        }
        else
        {
            Invoke("DisableSoundFx", time);
        }
    }

    public void StopCurrentClip()
    {
        audioSource.loop = false;
        audioSource.Stop();
        Invoke("DisableSoundFx", audioSource.clip.length + 0.01f);
    }

    private void DisableSoundFx()
    {
        GetComponent<PooledObject>().pool.ReturnObject(gameObject);
    }
}


