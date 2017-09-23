using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour {

	float speed = 5f;
	float angle;
	float pokeForce = 100f;
	public SphereCollider overlap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f) {
			angle = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f)) ;
			transform.position += transform.forward * Time.deltaTime * speed;
			//transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0 , Input.GetAxis("Horizontal")* Time.deltaTime * speed);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			Debug.Log(overlap.center);
			Collider[] hitColliders = Physics.OverlapSphere(overlap.gameObject.transform.position, overlap.radius);
			int i = 0;
			while (i < hitColliders.Length)
			{
				Debug.Log(hitColliders[i].gameObject.transform.name);
				if (!hitColliders[i].gameObject.CompareTag("Platform")) {
					Vector3 dir = hitColliders[i].transform.position - transform.position;
					hitColliders[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
					hitColliders[i].gameObject.GetComponent<Rigidbody>().AddForce(dir * pokeForce);
				}
				i++;
			}
		}
	}
}
