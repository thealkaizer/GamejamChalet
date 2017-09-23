using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Game : MonoBehaviour {

    public GameObject textStart;
    public GameObject image3;
    public GameObject image2;
    public GameObject image1;
    public GameObject textGo;

    private float countdown = 0;

    // Use this for initialization
    void Awake () {
        Time.timeScale = 0f;
        textStart.SetActive(true);
        image3.SetActive(false);
        image2.SetActive(false);
        image1.SetActive(false);
        textGo.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
                                  Time.timeScale = 1;
            textStart.SetActive(false);
        }
        countdown = countdown+Time.deltaTime;
               if (countdown >= 1 && countdown<2)
        {
            image3.SetActive(true);
        }
        if (countdown >= 2 && countdown < 3)
        {
            image3.SetActive(false);
            image2.SetActive(true);
        }
        if (countdown >= 3 && countdown < 4)
        {
            image2.SetActive(false);
            image1.SetActive(true);
        }
        if (countdown >= 4 && countdown < 5)
        {
           image1.SetActive(false);
            textGo.SetActive(true);
        }
        if (countdown >= 5 && countdown < 6)
        {
            textGo.SetActive(false);
                   }
    }
}
