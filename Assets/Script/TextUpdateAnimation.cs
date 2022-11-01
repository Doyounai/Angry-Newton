using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdateAnimation : MonoBehaviour
{
    public TextMeshProUGUI text;

    [Header("scale")]
    public LeanTweenType scaleEase;
    public float scaleDuration;
    public float scaling = 2;

    [Header("rotation")]
    public LeanTweenType rotationEase;
    public float roDuration;
    public Vector3 degreeTurn;


    private Vector3 defalutScale;
    private Vector3 defalutRotation;

    private void Awake()
    {
        defalutScale = transform.localScale;
        defalutRotation = transform.rotation.ToEuler();
    }
    public void setNewText(string messege)
    {
        transform.localScale = defalutScale;
        transform.rotation = Quaternion.Euler(defalutRotation.x, defalutRotation.y, defalutRotation.z);

        text.text = messege;

        transform.localScale = transform.localScale * scaling;
        transform.rotation = Quaternion.Euler(transform.rotation.x + degreeTurn.x, transform.rotation.y + degreeTurn.y, transform.rotation.z + degreeTurn.z);

        LeanTween.scale(gameObject, defalutScale, scaleDuration).setEase(scaleEase);
        LeanTween.rotateLocal(gameObject, defalutRotation, roDuration).setEase(rotationEase);
    }
}
