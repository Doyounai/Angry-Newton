using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleDotManager : MonoBehaviour
{
    public static AppleDotManager Instance;

    public GameObject DotPototype;
    public Transform dotParent;
    public float DotScale = 0.3f;   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateNewDot(Vector3 position)
    {
        if (dotParent == null)
            dotParent = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, transform).transform;

        Transform dot = Instantiate(DotPototype, position, Quaternion.identity, dotParent).transform;
        dot.localScale = Vector3.one * DotScale;
    }

    public void clearDotParent()
    {
        if (dotParent != null)
        {
            Destroy(dotParent.gameObject);
        }
    }
}
