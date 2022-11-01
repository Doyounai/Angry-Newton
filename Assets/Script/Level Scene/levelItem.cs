using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelItem : MonoBehaviour
{
    public string levelName;

    [Header("UI")]
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI scoreText;

    public void setText(string levelName, string levelNameText, string scoreText)
    {
        this.levelName = levelName;
        this.levelNameText.text = levelNameText;
        this.scoreText.text = scoreText;
    }

    public void loadScene()
    {
        transitionAnimation.Instance.gotoScene(levelName);
    }
}
