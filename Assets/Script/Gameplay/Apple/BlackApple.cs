using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackApple : Apple
{
    [Header("Black Apple")]
    public float radius = 5.0F;
    public float power = 10.0F;
    public float TimeExplosionCashDelay = 2f;
    private float ExplosionDelay = 0;
    public Gradient ExplosionColor;
    public Image sprite;
    public float explosionDamage = 2;
    public GameObject explosionParticle;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected override void Update()
    {
        base.Update();
        if(state == appleState.crah)
        {
            //cash();
        }
    }

    protected override void SetFollowCamera()
    {
        base.SetFollowCamera();
    }

    private void cash()
    {
        ExplosionDelay -= Time.deltaTime;

        float explosionDelayNomalize = ExplosionDelay * (1 / TimeExplosionCashDelay);
        //GetComponent<MeshRenderer>().material.color = ExplosionColor.Evaluate(explosionDelayNomalize);
        sprite.color = ExplosionColor.Evaluate(explosionDelayNomalize);

        if (ExplosionDelay <= 0f)
        {
            activeSkill();
        }
    }

    public override void activeSkill()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

                HpManager hp = hit.GetComponent<HpManager>();
                if(hp != null)
                    hp.TakeDamage(explosionDamage);
            }
        }

        base.activeSkill();

        CameraController.Instance.DefollowTarget();
        GameObject go = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(go, 1f);
        Destroy(gameObject);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //ExplosionDelay = TimeExplosionCashDelay;
        //state = appleState.crah;
        base.OnCollisionEnter(collision);
    }
}
