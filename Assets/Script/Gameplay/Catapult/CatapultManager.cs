using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CatapultController))]
public class CatapultManager : MonoBehaviour
{
    public new List<Apple> apples = new List<Apple>();
    private CatapultController controller;
    public Transform startApplePosition;
    public Vector3 appleAlignDirection;
    public float appleMargin;
    public float NextAppleDelay = 1f;

    private void Start()
    {
        controller = GetComponent<CatapultController>();
        SetNextApple();
    }

    public void Next()
    {
        StartCoroutine("NextApple");
    }

    public void SetNextApple()
    {
        if (apples.Count <= 0)
            return;

        controller.SetApple(apples[0]);
        apples.RemoveAt(0);
        UpdateApplePosition();
    }

    IEnumerator NextApple()
    {
        yield return new WaitForSeconds(NextAppleDelay);
        SetNextApple();
    }

    public void UpdateApplePosition()
    {
        for (int i = 0; i < apples.Count; i++)
        {
            apples[i].transform.position = startApplePosition.position + ((appleAlignDirection * appleMargin) * i);
        }
    }
}
