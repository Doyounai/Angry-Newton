using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Peerdech_Wongkum
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip music;
        public AudioMixerGroup mixer;

        private void Start()
        {
            SoundManger.Instance.PlaySound(music, music.length, true, mixer);
        }
    }
}