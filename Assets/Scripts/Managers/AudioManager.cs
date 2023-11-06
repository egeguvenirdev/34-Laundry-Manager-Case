using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void Init()
    {
        ActionManager.PlayAudio += OnPlaySound;
    }

    public void DeInit()
    {
        ActionManager.PlayAudio -= OnPlaySound;
    }

    private void OnPlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
