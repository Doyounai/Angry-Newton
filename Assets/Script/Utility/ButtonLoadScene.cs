using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        transitionAnimation.Instance.gotoScene(sceneName);
    }
}
