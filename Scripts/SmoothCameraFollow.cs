using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
	private Transform target;
	private Vector3 offset;
	private float maxBoardTilt = 15f;
	private Vector3 desiredPosition;
	private Transform desiredRotation;

	private float rotationDamping = 10f;
	private float movementDamping = 150f;

	private void Start()
	{
		offset = gameObject.transform.position;
		desiredPosition = gameObject.transform.position;
		desiredRotation = new GameObject("desiredRotation").transform;

		target = GameObject.Find("Player").transform;
	}

	// Update is called once per frame
	void LateUpdate()
    {
		Vector3 heading = transform.position - target.transform.position;
		heading.y = 0f;
		float distance = heading.magnitude;
		var direction = heading / distance;

		Vector3 rotationVectorRight = Vector3.Cross(direction, Vector3.up);
		desiredRotation.transform.position = target.transform.position + offset;
		desiredRotation.transform.RotateAround(target.transform.position, rotationVectorRight, -Input.GetAxisRaw("Vertical") * maxBoardTilt);
		desiredRotation.transform.LookAt(target.transform.position);
		desiredRotation.transform.RotateAround(desiredRotation.transform.position, direction, -Input.GetAxisRaw("Horizontal") * maxBoardTilt);
		transform.position = Vector3.Slerp(transform.position, desiredRotation.transform.position, Time.deltaTime * movementDamping);
		transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation.transform.rotation, Time.deltaTime * rotationDamping);
		CenterCamera();

	}

	private void CenterCamera()
	{
		Plane plane = new Plane(transform.forward, target.transform.position);
		Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));
		float distance;
		plane.Raycast(ray, out distance);

		Vector3 point = ray.GetPoint(distance);
		Vector3 offset = target.transform.position - point;
		transform.position += offset;
	}
}
