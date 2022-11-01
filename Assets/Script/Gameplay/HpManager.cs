using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HpManager : MonoBehaviour
{
    public float MaxHp;
    public float hp;
    public UnityEvent OnDie;

    private void Start()
    {
        hp = MaxHp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, MaxHp);

        if (hp <= 0)
        {
            die();
        }
    }

    void die()
    {
        OnDie.Invoke();
        Destroy(gameObject);
    }
}
