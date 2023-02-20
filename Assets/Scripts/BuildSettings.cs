using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettings : MonoBehaviour
{
    private void Start()
    {
        // #REVIEW: @Christian - target frame rates are useful in some cases. Though after our discussion yesterday
        // I believe this has been put in place to combat unreliable frame-by-frame executions that have now been 
        // resolved by use of coroutines. Let's remove the below.
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
