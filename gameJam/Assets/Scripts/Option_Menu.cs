using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Menu : MonoBehaviour
{
    public GameObject optionVisibility;

    public void ToggleOption()
    {
        optionVisibility.SetActive(false);
    }
}
