using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Wall : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FatAnimal")) {
            Destroy(other.gameObject);
        }
        
    }
}
