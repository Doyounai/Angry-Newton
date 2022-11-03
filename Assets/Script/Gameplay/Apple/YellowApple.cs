using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowApple : Apple
{
    [Header("Yellow Apple")]
    public float m_shiftVelocity;
    public Vector3 shiftedVelocity { get { return rb.velocity + (rb.velocity.normalized * m_shiftVelocity); } }

    public int AccDamage;
    public LayerMask redApple;
    public AudioClip skillSound;

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
        SoundManger.Instance.PlaySound(skillSound, 1.5f, mixer);
        base.activeSkill();
        base.SetFollowCamera();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Trail.enabled = false;
        
        if (collision.gameObject.CompareTag("Apple"))
        {
            if((redApple.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                collision.gameObject.GetComponent<Apple>().Grow();

                SpawnImpactParticle();
                CameraController.Instance.DefollowTarget();
                Destroy(gameObject);
            }
        }

        base.OnCollisionEnter(collision);
    }
}
