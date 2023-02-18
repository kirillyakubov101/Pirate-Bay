using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] ships = null;
	[SerializeField] Transform[] points= null;
	[SerializeField] int maxActiveShips = 7;
	[SerializeField] PatrolPath[] paths = null;
	public int currentShipsAmount = 0;

	public static ShipSpawner Instance { get; private set; }

	PlayerMovement player;

	float timer = 0f;

	private void Awake()
	{
		if (Instance == null)
		{
			player = FindObjectOfType<PlayerMovement>();
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		ships = Resources.LoadAll<GameObject>("Ships");
		
	}

	private void Update()
	{

		if(timer > 0.5f && currentShipsAmount < maxActiveShips)
		{
			SpawnShips();
			timer = 0f;
		}

		timer += Time.deltaTime;
		
	}

	private void SpawnShips()
	{
		
		int index1 = Random.Range(0, ships.Length);
		int index2 = Random.Range(0, points.Length);
		currentShipsAmount++;
		var instance = Instantiate(ships[index1], points[index2].position,Quaternion.identity);
		StartCoroutine(SlowDown(instance));

	}

	IEnumerator SlowDown(GameObject instance)
	{
		Enemy temp = instance.GetComponent<Enemy>();
		temp.level = Random.Range(player.level, player.level + 2);
		temp.SetNewStats();
		yield return new WaitForSeconds(1f);
		temp.path = GetFreePath();
		yield return new WaitForSeconds(1f);
		temp.isFindPath = true;
		temp.GoTo();


	}

	PatrolPath GetFreePath()
	{
		foreach(var path in paths)
		{
			if(!path.isPathTaken)
			{
				path.isPathTaken = true;
				return path;
			}
		}

		return null;
	}

}
