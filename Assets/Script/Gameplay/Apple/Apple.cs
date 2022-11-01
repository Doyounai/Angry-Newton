using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	public enum appleState
    {
		idle,
		shooted,
		skill,
		crah
    }

	[HideInInspector] public Rigidbody rb;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	public appleState state = appleState.idle;
	public float DeathDelay = 5;
	public float DotSpacingByTime = 0.1f;
	public float Damage;
	public GameObject impactParticle;
	public bool isItem = false;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

    protected virtual void Start()
    {
		if(!isItem)	
			DesactivateRb();
    }

	protected virtual void Update()
	{
		if (state == appleState.shooted && Input.GetMouseButtonDown(0))
		{
			activeSkill();
		}
	}

	public virtual void activeSkill()
	{
		state = appleState.skill;
	}

	public void Push(Vector3 force)
    {
		state = appleState.shooted;
		AppleDotManager.Instance.clearDotParent();

        rb.AddForce(force, ForceMode.Impulse);
		StartCoroutine("doting");
		SetFollowCamera();
    }

	protected virtual void SetFollowCamera()
    {
		CameraController.Instance.setFollowTarget(transform);
    }

    public void ActivateRb()
	{
		rb.isKinematic = false;
		this.GetComponent<Collider>().enabled = true;
	}

	public void DesactivateRb()
	{
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
		this.GetComponent<Collider>().enabled = false;
	}

    protected virtual void OnCollisionEnter(Collision collision)
    {
		if (state == appleState.crah)
			return;

		state = appleState.crah;
		StopCoroutine("doting");
		StartCoroutine("die");
    }

	private bool firstCash = true;

	public IEnumerator die()
    {
		yield return new WaitForSeconds(DeathDelay);
		//Destroy(gameObject);
		if(firstCash)
		{ 
			CameraController.Instance.DefollowTarget();
			firstCash = false;
		}
	}

    public IEnumerator doting()
    {
		while(true)
        {
            AppleDotManager.Instance.CreateNewDot(transform.position);
			yield return new WaitForSeconds(DotSpacingByTime);
        }
    }

	public void Grow()
    {
        LeanTween.scale(gameObject, transform.localScale * 4, 3f);
		LeanTween.delayedCall(5f, () =>
		{
			SpawnImpactParticle();
			Destroy(gameObject);
		});
    }

	public void SpawnImpactParticle()
    {
		GameObject particle = Instantiate(impactParticle, transform.position, Quaternion.identity);
		Destroy(particle, 1);
    }
}
