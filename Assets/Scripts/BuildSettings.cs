using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettings : MonoBehaviour
{
    // Removed frame rate cap 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
