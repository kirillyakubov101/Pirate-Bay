using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
	public Transform player = null;

	private void LateUpdate()
	{
		if(player == null) { return; }
		Vector3 newPos = player.transform.position;
		newPos.y = transform.position.y;
		transform.position = newPos;
	}


}
