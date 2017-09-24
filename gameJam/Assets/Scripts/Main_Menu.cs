using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{

    public GameObject optionVisibility;
    public GameObject CreditVisibility;
    public GameObject CreditExit;

    // Use this for initialization
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
   public void ToggleOption()
    {
        optionVisibility.SetActive(true);
    }
    public void ToggleCreditIn()
    {
        CreditVisibility.SetActive(true);
    }
    public void ToggleCreditOut()
    {
        CreditExit.SetActive(false);
    }
}
