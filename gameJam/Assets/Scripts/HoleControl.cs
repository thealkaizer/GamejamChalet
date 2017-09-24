using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControl : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    public GameManager gameManager;

    public GameObject black;

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
        this.holeCollider = this.GetComponent<Collider>();
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
        if(this.currentOpeningTimer <= this.openingSpeed) {
            // Here, hole is opening
            this.isOpening = true;
            // TODO Scale to open
        }
        else if(this.currentOpeningTimer <= (this.openingSpeed + this.closingSpeed)) {
            // Here, hole is full open
            this.isOpening = false;
        }
        else if(this.currentOpeningTimer <= (this.openingSpeed + this.closingSpeed + this.closingSpeed)){
            // Now, hole is closing
            this.isClosing = true;
            // TODO Scale to close
        }
        else {
            // Here, means hole must close now
            black.transform.position = new Vector3(0,0,-0.04f);
            this.isOpen = false;
            this.isClosing = false;
            this.isOpening = false;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.transform.CompareTag("FatAnimal")) {
            collider.enabled = false;
            AnimalControl animal = collider.gameObject.GetComponent<AnimalControl>();
            if(animal.id == animal.cat) {
                gameManager.addOneCat();
            }
            else {
                this.gameManager.addOneDog();
            }
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
            black.transform.position = new Vector3(0,0,0.04f);
            return true;
        }
        return false; // Meaning it is already open
    }
}