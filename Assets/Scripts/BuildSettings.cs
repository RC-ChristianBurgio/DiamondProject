using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettings : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
