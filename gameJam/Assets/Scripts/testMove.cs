using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class testMove : MonoBehaviour {

	internal Player CharacterPlayer;
	private bool pokeInput, hammerInput;


	float speed = 5f;
	float angle;
	float pokeForce = 100f;
	public SphereCollider overlap;

	// Use this for initialization
	void Start () {
		CharacterPlayer = ReInput.players.GetPlayer(0);
	}
	
	// Update is called once per frame
	void Update () {

		pokeInput = CharacterPlayer.GetButtonDown("Poke");
		hammerInput = CharacterPlayer.GetButtonDown("Hammer");

		if (CharacterPlayer.GetAxis("Character Horizontal Axis") != 0.0f || CharacterPlayer.GetAxis("Character Vertical Axis") != 0.0f) {
			angle = Mathf.Atan2(CharacterPlayer.GetAxis("Character Horizontal Axis"), CharacterPlayer.GetAxis("Character Vertical Axis")) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f)) ;
			transform.position += transform.forward * Time.deltaTime * speed;
		}

		if (pokeInput) {
			Debug.Log("POKE");
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

		if (hammerInput) {
			Debug.Log("HAMMEr");
		}
	}
}
