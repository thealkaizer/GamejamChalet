using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.transform.CompareTag("FatAnimal")) {
			col.enabled = false;

			//changé nombre de chien/chat
			Destroy(col.gameObject, 2f);
		};
	}
}
