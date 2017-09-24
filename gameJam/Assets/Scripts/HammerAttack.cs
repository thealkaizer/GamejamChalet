using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;
using UnityEngine.UI;

public class HammerAttack : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------

    //UI
    public Image radialCooldown;

    //Feedback
    public GameObject dust;

    //InputControls
    internal Player CharacterPlayer;
	private bool pokeInput, hammerInput;

    //SphereColliders
    public SphereCollider pokeHitPoint;
    public SphereCollider hammerHitPoint;
    public SphereCollider hammerCrushArea;
    public SphereCollider hammerPushArea;

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
    public float reducFactor            = 2f;
    bool hitAnimal                      = false;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------

    void Start() {
        CharacterPlayer = ReInput.players.GetPlayer(0);
    }

	// Update is called once per frame
	void Update () {
        this.updateAllColdowns();
        pokeInput = CharacterPlayer.GetButtonDown("Poke");
		hammerInput = CharacterPlayer.GetButtonDown("Hammer");

        // Handle inputs
        if(pokeInput) {
            Debug.Log("POKE");
            this.pokeAttack();
        }
        else if(hammerInput) {
            Debug.Log("Hammer");
            this.bigHammerAttack();
        }
    }

    /** Process a simple poke attack */
    private void pokeAttack() {
        AkSoundEngine.PostEvent("Play_Push_Animals", gameObject);
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

        //Feedback
        Camera.main.DOShakePosition(0.4f, 0.2f, 10, 80, true);
        GameObject dustInstance = Instantiate(dust, new Vector3(hammerHitPoint.gameObject.transform.position.x, hammerHitPoint.gameObject.transform.position.y +0.4f, hammerHitPoint.gameObject.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
        Destroy(dustInstance, 3f);

        this.hammerCurrentTimer = 0;
        this.hammerIsReady = false;
        Collider[] hitColliders = Physics.OverlapSphere(hammerHitPoint.gameObject.transform.position, hammerPushArea.radius);
        hitAnimal = false;
        for(int i = 0;i < hitColliders.Length;i++) {
            Collider currentCollider = hitColliders[i];
            if(currentCollider.gameObject.CompareTag("FatAnimal")) {
                hitAnimal = true;
                if (currentCollider.gameObject.GetComponent<AnimalControl>().id == 1) {
                    AkSoundEngine.PostEvent("SFX_Hit_Cat", gameObject);
                } else {
                    AkSoundEngine.PostEvent("SFX_Hit_Dog", gameObject);
                }
                Rigidbody rb = currentCollider.gameObject.GetComponent<Rigidbody>();
                Vector3 directionVector = currentCollider.transform.position - hammerHitPoint.gameObject.transform.position;
                float distance = Vector3.Distance(currentCollider.transform.position, hammerHitPoint.gameObject.transform.position);
                if(distance <= hammerCrushArea.radius) {
                    Debug.Log("Crushed (Not implemented yet)");
                    // In this case, is in crush area and mush apply the famous crush punishment!
                    // Dev note: to be crushed, the center of animal position must be in crush area (Not just the collider)
                    // TODO: Add action (And kill the poor animal)
                }
                else {
                    // In this case, in push area but not in crush area
                    Vector3 directionNoraml = Vector3.Normalize(directionVector);
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce((Vector3.up * hammerForceVertical) / Mathf.Pow(distance, reducFactor));
                    rb.AddForce((directionNoraml * hammerForceHorizontal) / Mathf.Pow(distance, reducFactor));
                }
            }
        }
            if (hitAnimal == true) {
                Debug.Log("ANIMAL");
                AkSoundEngine.PostEvent("Play_Hit_Animals", gameObject);
            } else {
                Debug.Log("GROUND");
                AkSoundEngine.PostEvent("Play_Hit_Ground", gameObject);
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
        radialCooldown.fillAmount = (this.hammerCurrentTimer / hammerColdown);
        if(this.hammerCurrentTimer >= this.hammerColdown) {
            if (this.hammerIsReady == false) {
                radialCooldown.fillAmount = 1f;
                radialCooldown.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f),  0.35f, 3, 0f);
            }
            this.hammerIsReady = true;
        }
        
    }
}
