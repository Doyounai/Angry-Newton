using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCall : MonoBehaviour
{
    #region singaton
    public static ScoreCall Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public void saveScore(int level, int score)
    {
        PlayerPrefs.SetInt("level" + level, score);
    }
}
