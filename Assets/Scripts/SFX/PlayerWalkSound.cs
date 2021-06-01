using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSound : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    public List<AudioClip> WalkClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (playerMovement.currentSpeed == playerMovement.walkSpeed)
            {
                PlayMoveClip("sfx_walk");
            }
            else if (playerMovement.currentSpeed == playerMovement.runSpeed)
            {
                PlayMoveClip("sfx_run");
            }
            else if (playerMovement.currentSpeed == playerMovement.SuperSpeed)
            {
                PlayMoveClip("sfx_run");
            }
           
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            audioSource.Stop();
        }
        

    }

    void PlayMoveClip(string soundName)
    {
        audioSource.clip = FindClip(soundName);
    }

    AudioClip FindClip(string soundName)
    {
        foreach (var clip in WalkClips)
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
