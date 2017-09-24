using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControl : MonoBehaviour {
    public float sinkingTime = 2f;

    private bool isOpening;
    private bool isClosing;
    private bool isOpen;
    private float openingDuration;

	// Use this for initialization
	void Start () {
        this.isOpen = false;
        this.isClosing = false;
        this.isOpening = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.transform.CompareTag("FatAnimal")) {
            collider.enabled = false;
			// TODO changé nombre de chien/chat
			Destroy(collider.gameObject, sinkingTime);
		};
	}

    public bool openHole(float duration) {
        if(!this.isOpen) {
            this.isOpen = true;
            this.openingDuration = duration;
            return true;
        }
        return false;
    }
}