using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelItemsManager : MonoBehaviour
{
    public GameObject levelItemPototype;
    public int maxLevel;

    private void Start()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            GameObject item = Instantiate(levelItemPototype, transform);
            item.GetComponent<levelItem>().setText(
                "level" + (i + 1).ToString(), 
                "level " + (i + 1).ToString(),
                PlayerPrefs.GetInt("level" + (i + 1), 0).ToString()
                );
        }
    }
}
