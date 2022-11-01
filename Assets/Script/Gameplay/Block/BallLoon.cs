using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLoon : Block
{
    public void OnDie()
    {
        Destroy(gameObject);
    }
}
