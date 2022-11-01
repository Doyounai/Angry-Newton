using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowApple : Apple
{
    [Header("Yellow Apple")]
    public float m_shiftVelocity;
    public Vector3 shiftedVelocity { get { return rb.velocity + (rb.velocity.normalized * m_shiftVelocity); } }

    public int AccDamage;

    //tail
    [Header("Bullet Tail")]
    public TrailRenderer Trail;
    public BulletTrailScriptableObject TrailConfig;

    protected override void Start()
    {
        ConfigureTrail();
        Trail.enabled = false;
        base.Start();
    }

    private void ConfigureTrail()
    {
        if (Trail != null && TrailConfig != null)
        {
            TrailConfig.SetupTrail(Trail);
        }
    }

    protected override void SetFollowCamera()
    {
        //base.SetFollowCamera();
    }

    public override void activeSkill()
    {
        rb.velocity = shiftedVelocity;
        Damage = AccDamage;
        Trail.enabled = true;
        base.activeSkill();
        base.SetFollowCamera();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Trail.enabled = false;
        base.OnCollisionEnter(collision);
    }
}
