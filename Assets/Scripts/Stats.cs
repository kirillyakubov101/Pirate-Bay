using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Stat/Stat",menuName = "Stat")]
public class Stats: ScriptableObject
{
	public float hp;
	public float damage;
	public float reloadTime;
	public int level;
	public float attackRange;
	
}
