using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private HpManager _HpManager;
    private Rigidbody rb;
    public float OnDamageInteval;
    public float SelftHitDe = 0.6f;
    public GameObject dieParticle;
    public int score = 100;

    private bool IsContactApple = false;
    private float damage;

    IEnumerator Start()
    {
        _HpManager = GetComponent<HpManager>();
        rb = GetComponent<Rigidbody>();
        IsContactApple = true;

        yield return new WaitForEndOfFrame();
        IsContactApple = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // hit apple
        if (collision.gameObject.CompareTag("Apple"))
        {
            damage = collision.gameObject.GetComponent<Apple>().Damage;
            IsContactApple = true;
            StartCoroutine("OnContactApple");
        }

        // self force
        if (!IsContactApple)
        {
            //Debug.Log(rb.velocity.magnitude * SelftHitDe);
            _HpManager.TakeDamage(rb.velocity.magnitude * SelftHitDe);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            IsContactApple = false;
            StopCoroutine("OnContactApple");
        }
    }
    
    public void SpawnDieParticle()
    {
        GameObject go = Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(go, 1f);
    }

    IEnumerator OnContactApple()
    {
        while (true)
        {
            _HpManager.TakeDamage(damage);
            yield return new WaitForSeconds(OnDamageInteval);
        }
    }

    public void getScore()
    {
        ScoreManager.Instance.GetScore(score);
    }
}
