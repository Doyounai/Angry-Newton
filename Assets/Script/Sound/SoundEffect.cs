using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffect : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float duration)
    {
        PlaySound(clip, duration, false, null);
    }

    public void PlaySound(AudioClip clip, float duration, AudioMixerGroup mixer)
    {
        PlaySound(clip, duration, false, mixer);
    }

    public void PlaySound(AudioClip clip, float duration, bool loop, AudioMixerGroup mixer)
    {
        source.clip = clip;
        source.loop = loop;
        if (mixer != null)
            source.outputAudioMixerGroup = mixer;

        source.Play();

        Destroy(gameObject, duration);
    }
}
