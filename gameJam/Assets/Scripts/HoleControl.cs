using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControl : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    public float animalSinkingTime = 2f;

    public float    openingSpeed;
    public float    closingSpeed;
    public int      minOpenDuration;
    public int      maxOpenDuration;

    public bool     isOpen = false;
    private bool    isOpening = false;
    private bool    isClosing = false;
    private float   openDuration;

    private float   currentOpeningTimer; // Internally used
    private Collider holeCollider;


    // ------------------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------------------

    private void Start() {
        this.holeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if(!isOpen) {   
            this.currentOpeningTimer = 0;
            this.holeCollider.enabled = false;
            return;
        }
        this.currentOpeningTimer += Time.deltaTime;
        if(this.currentOpeningTimer <= this.openDuration) {
            this.isOpening = true;
            // Scale to open
        }
        else if(this.currentOpeningTimer <= this.closingSpeed) {
            this.isOpening = false;
        }
        else {
            this.isClosing = true;
            // Scale to close
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.transform.CompareTag("FatAnimal")) {
            collider.enabled = false;
            // TODO changé nombre de chien/chat
            Destroy(collider.gameObject, animalSinkingTime);
        };
    }

    public bool openHole() {
        if(!this.isOpen) {
            this.isOpen = true;
            this.isOpening = true;
            this.isClosing = false;
            this.openDuration = Random.Range(minOpenDuration, maxOpenDuration);
            this.holeCollider.enabled = true;
            return true;
        }
        return false; // Meaning it is already open
    }
}