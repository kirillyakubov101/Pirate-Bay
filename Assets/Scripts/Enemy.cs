using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
	public bool isAggro = false;
	public PatrolPath path;
	public bool isFindPath = false;
	Vector3 pathLocation;

	public bool isOnTheWay = false;

	private int carryGold = 100;

	private void Start()
	{
		carryGold += level * 5;
		totalDamage += stat.damage;
	}

	public int currentPathIndex = 0;

	private void Update()
	{
		if(!isAggro)
		{
			if (isFindPath)
			{
				PatrolBehaviour();
			}

		}
	}

	public void GoTo()
	{
		if (isOnTheWay) { return; }
		isOnTheWay = true;
		pathLocation = path.GetWaypoint(currentPathIndex);
		agent.SetDestination(pathLocation);
	}

	public void EngageTarget()
	{
		agent.enabled = false;
		StartCoroutine(TurnTowardsTarget());
		Attack(stat.damage);
	}

	private void PatrolBehaviour()
	{
		if(target != null)
		{

			agent.isStopped = true;
			agent.velocity = Vector3.zero;
		}
		//pathLocation = path.GetWaypoint(currentPathIndex);

		//agent.SetDestination(pathLocation);

		float distance = Vector3.Distance(transform.position, path.GetWaypoint(currentPathIndex));
		if (distance <= 20f)
		{
			isOnTheWay = false;
			currentPathIndex++;
			if(currentPathIndex == path.Count) { currentPathIndex = 0; }
			GoTo();
		}

	}

	public void SetNewStats()
	{
		this.hitpoints = this.level * this.hitpoints + 50;
		maxHitpoints = this.hitpoints;
		this.totalDamage += this.level * this.totalDamage / 4;
	}

	public void Award(ref PlayerMovement player)
	{
		player.level++;
		player.levelUpVFX.Play();
		LevelController.Instance.GainGold(carryGold);
	}

}
