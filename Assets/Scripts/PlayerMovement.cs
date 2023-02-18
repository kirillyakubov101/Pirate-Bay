using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : LivingEntity
{
	[SerializeField] LayerMask enemyMask = new LayerMask();
	[SerializeField] LayerMask water = new LayerMask();
	[SerializeField] LayerMask shopMask = new LayerMask();
	[SerializeField] LayerMask cursorMask = new LayerMask();
	
	[SerializeField] UnityEvent onShopEnter = null;

	public ParticleSystem levelUpVFX = null;
	private Vector3 destination;

	private void Update()
	{
		if (target != null) { return; }
		if (Shop.Instance.isShopOpen) { return; }

		UpdateCursor();

		if (MovesToShop())
		{
			ProceedToShop(Shop.Instance.transform.position);
		}
		if (ProcessCombat()) { return; }
		if (ProcessMovement()) { return; }


	}

	private void UpdateCursor()
	{
		if(Physics.Raycast(GetMouseRay(), out RaycastHit hit, Mathf.Infinity, cursorMask))
		{
			int layer = hit.collider.gameObject.layer;
			if (layer == 3)
			{
				LevelController.Instance.SetAttackCursor();
			}
			else if(layer == 6)
			{
				LevelController.Instance.SetShopCursor();
			}
			else
			{
				LevelController.Instance.SetNormalCursor();
			}
		}

		
	}

	private bool MovesToShop()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, Mathf.Infinity, shopMask))
			{
				return true;
			}
		}
			
		return false;
	}

	private void ProceedToShop(Vector3 shopCord)
	{
		if (Vector3.Distance(transform.position, shopCord) <= 400)
		{
			agent.enabled = false;
			Shop.Instance.isShopOpen = true;
			onShopEnter?.Invoke();
		}
	}

	bool ProcessCombat()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(GetMouseRay(), out RaycastHit hit,Mathf.Infinity,enemyMask))
			{
				var possibleTarget = hit.collider.GetComponent<LivingEntity>();
				if(possibleTarget == null || possibleTarget.agent == null) { return false; }
				AssignTarget(possibleTarget);

				BackgroundMusic.Instance.audioSource.clip = BackgroundMusic.Instance.fightSong;
				BackgroundMusic.Instance.audioSource.Play();
			
				StartCoroutine(ProceedTowardsTarget(possibleTarget));
				return true;
			}
		}
		
		return false;
	}

	IEnumerator ProceedTowardsTarget(LivingEntity possibleTarget)
	{
		if(agent == null) { yield return null; }
		agent.enabled = true;
		possibleTarget.agent.isStopped = true;
		while ((Vector3.Distance(transform.position, target.transform.position) > stat.attackRange + 60))
		{
			agent.SetDestination(target.transform.position);
			yield return null;
		}


		LevelController.Instance.SetNormalCursor();
		agent.isStopped = true;
		agent.enabled = false;
		possibleTarget.AssignTarget(this);
		Enemy enemy = (Enemy)possibleTarget;
		enemy.EngageTarget();
		enemy.isAggro = true;
		

		yield return TurnTowardsTarget();
		Attack(totalDamage);

	}

	



	bool ProcessMovement()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, Mathf.Infinity, water))
			{
				if(target != null) { return false; }

				LevelController.Instance.cursorMove.transform.position = hit.point;
				LevelController.Instance.cursorMove.Play();

				agent.enabled = true;
				agent.isStopped = false;
				agent.ResetPath();
				agent.autoRepath = true;
				destination = hit.point;
				agent.SetDestination(destination);
				return true;
			}

		}
		return false;
	}

	private Ray GetMouseRay()
	{
		return Camera.main.ScreenPointToRay(Input.mousePosition);
	}
}
