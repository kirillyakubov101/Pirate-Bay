using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorld : MonoBehaviour
{
    float inputX, inputY,mouseScroll;
    public float cameraSpeed = 10f;
	public float scrollSpeed = 10f;
	public float maxHeight = 300f;
	public float mixHeight = 0;
	public GameObject IntorCamera = null;
	float scrollAmount;

	public bool isSwitched = false;

	private void Start()
	{
		isSwitched = false;
	}

	private void Update()
	{
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");
		scrollAmount = mouseScroll * scrollSpeed;

	}


	public void SetSwitchCamera()
	{
		StartCoroutine(SwitchCameraDelay());
	}

	IEnumerator SwitchCameraDelay()
	{
		yield return new WaitForSeconds(1.5f);
		isSwitched = true;
	}


	void LateUpdate()
    {
		if (IntorCamera.activeSelf) { return; }
		Vector3 inputPos = new Vector3(inputX, -scrollAmount, inputY);
		if (!isSwitched) { return; }
		Camera.main.transform.position += inputPos * cameraSpeed * Time.deltaTime;
		transform.position = Camera.main.transform.position;
	}

	
}
