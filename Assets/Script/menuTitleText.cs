using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuTitleText : MonoBehaviour
{
    private void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.4f, 0.7f).setLoopPingPong();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            transitionAnimation.Instance.gotoScene("selectLevel");
        }
    }
}
