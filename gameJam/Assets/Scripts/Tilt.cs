using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour {

	public float maxTilt = 20f;
	public float tiltSpeed = 1f;
	public float targetTiltHorizontal, targetTiltVertical;
	private float directionTiltHorizontal, directionTiltVertical;
	public Transform platform;
	// Use this for initialization
	void Start () {
		platform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		targetTiltHorizontal = Input.GetAxis("Horizontal") * maxTilt;
		targetTiltVertical = Input.GetAxis("Vertical") * maxTilt;

		//platform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(platform.rotation.x, targetTiltVertical, tiltSpeed * Time.deltaTime), 0 , Mathf.Lerp(platform.rotation.z, -targetTiltHorizontal, tiltSpeed * Time.deltaTime)));

		platform.rotation = Quaternion.Lerp(platform.rotation, Quaternion.Euler(targetTiltVertical, 0 , -targetTiltHorizontal), Time.deltaTime * tiltSpeed);
	}
}
