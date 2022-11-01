using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionAnimation : MonoBehaviour
{
    #region sigaton
    public static transitionAnimation Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public Animator animator;
    public SceneManagement sceneManagement;
    private string sceneToGo;

    public void gotoScene(string sceneToGo)
    {
        this.sceneToGo = sceneToGo;
        animator.SetTrigger("fade");
    }

    public void loadScene()
    {
        sceneManagement.GotoScene(sceneToGo);
    }
}
