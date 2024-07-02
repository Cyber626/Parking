using UnityEngine;

public class WheelsControl : MonoBehaviour {

	Vector3 localAngle;
	public float maxSteerAngle = 30f;
	float steerAngle ;

	// Update is called once per frame
	void Update () {
		steerAngle = - Input.GetAxis ("Horizontal") * maxSteerAngle;
	}

	void LateUpdate()
	{
		localAngle = transform.localEulerAngles;
		localAngle.z = steerAngle;
		transform.localEulerAngles = localAngle;
	}
}
