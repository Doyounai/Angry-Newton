using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transition : MonoBehaviour
{
    public Sprite[] apple;
    public Image image;

    public void randomNewImage()
    {
        image.sprite = apple[Random.Range(0, apple.Length - 1)];
    }
}
