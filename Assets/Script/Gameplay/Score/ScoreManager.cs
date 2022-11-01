using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singaton
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public TextUpdateAnimation text;
    public int score = 0;

    public void GetScore(int score)
    {
        this.score += score;
        text.setNewText(this.score.ToString());
    }
}
