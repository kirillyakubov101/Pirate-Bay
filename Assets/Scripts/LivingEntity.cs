using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
	[SerializeField] protected Stats stat = null;
	[SerializeField] ParticleSystem[] cannons = null;
	[SerializeField] ParticleSystem hitEffect = null;
	[SerializeField] GameObject deathVFX = null;
	[SerializeField] ParticleSystem deathExplosionVFX = null;

	[System.Serializable]
	public class onHit: UnityEvent<float>
	{

	}

	[SerializeField] onHit HitEvent = null;
	public float totalDamage = 0;
	public float maxHitpoints;
	public float hitpoints;
	public int level = -1;
	private const float turnSpeed = 5f;
	private bool isDead = false;

	public LivingEntity target;

	[HideInInspector]
	public NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		hitpoints = stat.hp;
		maxHitpoints = hitpoints;
		level = stat.level;
		totalDamage += stat.damage;
	}

	

	public bool IsDead()
	{
		return hitpoints <= 0f;
	}

	public void TakeDamage(float dmg)
	{
		HitEvent?.Invoke(dmg);
		hitpoints = Mathf.Clamp(hitpoints, 0f, hitpoints - dmg);
		if(hitpoints <= 0f)
		{
			isDead = true;
			Die();
		}
	}

	protected IEnumerator TurnTowardsTarget()
	{
		float timer = 0f;
		while (Mathf.Abs(Vector3.SignedAngle(transform.position, target.transform.position, Vector3.up)) >= 5f && timer <= 1f)
		{
			var lookPos = target.transform.position - transform.position;

			var rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

			timer += Time.deltaTime;
			yield return null;
		}

		float timer2 = 0;

		while(timer2 < 1f)
		{
			transform.Rotate(Vector3.up, Time.deltaTime * 90f);
			timer2 += Time.deltaTime;
			yield return null;
		}
	

	}

	public void AssignTarget(LivingEntity _target)
	{
		this.target = _target;
	}


	protected void Shoot()
	{
		hitEffect.Play();
		foreach(var ele in cannons)
		{
			ele.Play();
		}
	}

	public void Attack(float _dmg)
	{
		StartCoroutine(AttackBehaviour());
	}

	private IEnumerator AttackBehaviour()
	{ 
		
		while (!isDead && !target.isDead)
		{
			Shoot();
			yield return new WaitForSeconds(stat.reloadTime);
			target.TakeDamage(totalDamage);
			
		}

	}

	public float GetHealthPercentage()
	{
		return hitpoints / maxHitpoints;
	}

	private void Die()
	{
		if(this is PlayerMovement)
		{
			StartCoroutine(ProceedDeathSquance());
			

		}

		if(this is Enemy)
		{
			Destroy(agent);
			BackgroundMusic.Instance.audioSource.clip = BackgroundMusic.Instance.RandomClip();
			BackgroundMusic.Instance.audioSource.Play();

			var player = (PlayerMovement)target;
			((Enemy)this).Award(ref player);
			if(((Enemy)this).path == null) { return; }
			((Enemy)this).path.isPathTaken = false;
			ShipSpawner.Instance.currentShipsAmount--;
		}
		deathVFX.SetActive(true);
		deathExplosionVFX.Play();
		StartCoroutine(ProceedDeathSquance());
	}

	private IEnumerator ProceedDeathSquance()
	{
		float timer = 0f;
		while(timer < 5f)
		{
			transform.Rotate(Vector3.right, Time.deltaTime * 20f);
			transform.Translate(Vector3.down * Time.deltaTime * 7f);
			timer += Time.deltaTime;
			yield return null;
		}

		if (this is PlayerMovement)
		{
			
			SceneManager.LoadSceneAsync(0);
		}

		Destroy(gameObject); 
	}
}
