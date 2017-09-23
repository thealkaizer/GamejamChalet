using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    public SphereCollider pokeHitPoint;
    public SphereCollider hammerHitPoint;
    public SphereCollider hammerCrushPoint;

    // Pole attack
    public float pokeColdown            = 1f; // Reload time in seconds
    private float pokeCurrentTimer      = 0f;
    private bool pokeIsReady            = true;
    public float pokeForce              = 100f;

    
    // Hammer attack
    public float hammerColdown          = 4f; // Reload time in seconds
    private float hammerCurrentTimer    = 0f;
    private bool hammerIsReady          = true;
    public float hammerForceVertical    = 100f;
    public float hammerForceHorizontal  = 100f;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------

	// Update is called once per frame
	void Update () {
        this.updateAllColdowns();

        // Handle inputs
        if(Input.GetKeyDown(KeyCode.E)) {
            this.pokeAttack();
        }
        else if(Input.GetKeyDown(KeyCode.T)) {
            this.bigHammerAttack();
        }
    }

    /** Process a simple poke attack */
    private void pokeAttack() {
        if(!this.pokeIsReady) {
            // Not available yet
            // TODO Play a sound or something (UI warning..)?
            return;
        }
        this.pokeCurrentTimer = 0;
        this.pokeIsReady = false;
        //Debug.Log(overlap.center);
        Collider[] hitColliders = Physics.OverlapSphere(pokeHitPoint.gameObject.transform.position, pokeHitPoint.radius);
        for(int i = 0; i < hitColliders.Length; i++) {
            if(hitColliders[i].gameObject.CompareTag("FatAnimal")) {
                Vector3 dir = hitColliders[i].transform.position - transform.position;
                dir = Vector3.Normalize(dir);
                hitColliders[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                hitColliders[i].gameObject.GetComponent<Rigidbody>().AddForce(dir * pokeForce);
            }
        }
    }

    /** Process a big hammer attack */
    private void bigHammerAttack() {
        if(!this.hammerIsReady) {
            // Not available yet
            // TODO Play a sound or something (UI warning..)?
            return;
        }
        this.hammerCurrentTimer = 0;
        this.hammerIsReady = false;
        Collider[] hitColliders = Physics.OverlapSphere(hammerHitPoint.gameObject.transform.position, hammerHitPoint.radius);
        for(int i = 0;i < hitColliders.Length;i++) {
            if(hitColliders[i].gameObject.CompareTag("FatAnimal")) {
                Rigidbody rb = hitColliders[i].gameObject.GetComponent<Rigidbody>();
                Vector3 dir = hitColliders[i].transform.position - hammerHitPoint.gameObject.transform.position;
                dir = Vector3.Normalize(dir);
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce((Vector3.up * hammerForceVertical));
                rb.AddForce((dir * hammerForceHorizontal));
            }
        }
    }

    private void updateAllColdowns() {
        // Poke coldown
        this.pokeCurrentTimer += Time.deltaTime;
        if(this.pokeCurrentTimer >= this.pokeColdown) {
            this.pokeIsReady = true;
        }

        // Hammer coldown
        this.hammerCurrentTimer += Time.deltaTime;
        if(this.hammerCurrentTimer >= this.hammerColdown) {
            this.hammerIsReady = true;
        }
    }
}
