using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotation : MonoBehaviour
{
	public Vector3 twist;

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(twist * Time.deltaTime);
    }
}
