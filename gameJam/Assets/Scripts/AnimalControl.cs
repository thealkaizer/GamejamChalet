using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: this script is almost useless, just to get the id of the animal to know if it's a cat or dog
public class AnimalControl : MonoBehaviour {
    public int id; // -1 for dog, 1 for cat
    public int cat = 1;
    public int dog = -1;

    public bool landed;

    void Start() {
         GetComponent<Rigidbody>().AddForce(Vector3.down * 25, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Platform")) {
            if (landed == false) {
                StartCoroutine("fixGravity");
            }

        }
    }

    IEnumerator fixGravity() {
        Debug.Log("fixgravity");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * 25);
        yield return new WaitForFixedUpdate();
        GetComponent<Rigidbody>().useGravity = true;
        landed = true;
    }
}
