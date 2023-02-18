using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	const float raduis = .3f;
	public bool isPathTaken = false;

	public int Count { get { return transform.childCount; }}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			int j = GetNextIndex(i);
			Gizmos.DrawSphere(transform.GetChild(i).position, raduis);
			Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
		}

	}

	public int GetNextIndex(int i)
	{
		if (i == transform.childCount - 1)
		{
			return 0;
		}

		return i + 1;
	}

	public Vector3 GetWaypoint(int i)
	{
		return transform.GetChild(i).position;
	}


}
