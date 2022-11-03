using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class newtonCounter : MonoBehaviour
{
    public CatapultManager catapult;
    public int newton = 0;
    public int maxNewton;
    public GameObject skipButton;
    public Image newtonBar;
    public TextMeshProUGUI newtonText;

    private void Awake()
    {
        skipButton.SetActive(false);
        newtonText.text = newton + " / " + maxNewton;
    }

    public void newtonHit()
    {
        newton += 1;
        newtonBar.fillAmount = (float)newton * (1 / (float)maxNewton);
        newtonText.text = newton + " / " + maxNewton;

        if (newton >= maxNewton)
        {
            skipButton.SetActive(true);
        }
    }
}
