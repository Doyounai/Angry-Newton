using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class buttonEffect : MonoBehaviour
{
    int effectId = -1;
    Vector3 originScale;

    [Header("Pop up effect")]
    public Vector3 scalingTo = new Vector3(1.2f, 1.2f, 1.2f);
    public LeanTweenType type = LeanTweenType.easeOutSine;
    public float duration = 0.2f;

    [Header("Sound Effect")]
    public AudioClip activeSound;
    public AudioClip Click;
    public AudioMixerGroup mixer;

    private void Start()
    {
        originScale = transform.localScale;
    }

    public void OnDown()
    {
        //SoundManger.Instance.PlaySound(Click, 1, mixer);
    }

    public void OnMouseOver()
    {
        if (effectId != -1)
            LeanTween.cancel(effectId);

        effectId = LeanTween.scale(gameObject, scalingTo, duration).setEase(type).id;
        //SoundManger.Instance.PlaySound(activeSound, 1, mixer);
    }

    public void OnMouseExit()
    {
        if (effectId != -1)
            LeanTween.cancel(effectId);

        effectId = LeanTween.scale(gameObject, originScale, duration).setEase(type).id;
    }
}