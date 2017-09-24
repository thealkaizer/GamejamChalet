using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Check SWITCH
		 AkSoundEngine.PostEvent("Play_Musique_cats_dogs_01", gameObject);
		 AkSoundEngine.PostEvent("Play_Ambiances", gameObject);
		 AkSoundEngine.SetSwitch("Balance", "Green", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
