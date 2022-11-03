using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManger : MonoBehaviour
{
    #region sigaton
        public static SoundManger Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
    public GameObject soundPlayer;
    public bool isPlaySoundStart;
    public AudioMixerGroup mixer;
    public AudioClip startSound;
    public float volume = 1f;

    private void Start()
    {
        if (!isPlaySoundStart)
        {
            return;
        }

        LeanTween.delayedCall(1.5f, () =>
        {
            PlaySound(startSound, startSound.length, mixer);
        });
    }

    public void PlaySound(AudioClip clip, float duration, AudioMixerGroup mixer)
    {
        PlaySound(clip, duration, false, mixer);
    }

    public void PlaySound(AudioClip clip, float duration, bool loop, AudioMixerGroup mixer)
    {
        SoundEffect Go = Instantiate(soundPlayer).GetComponent<SoundEffect>();
        StartCoroutine(play(clip, duration, loop, mixer, Go));
    }

    IEnumerator play(AudioClip clip, float duration, bool loop, AudioMixerGroup mixer, SoundEffect go)
    {
        yield return new WaitForEndOfFrame();
        go.PlaySound(clip, duration, loop, mixer);
    }
}