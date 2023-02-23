using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Hand;

public class LocalPlayerHandler : NetworkBehaviour
{
    [SerializeField] GameObject[] go;
    [SerializeField] GameObject[] jointPoseGO;

    void Start()
    {
        if (isLocalPlayer)
        {
            foreach (GameObject GO in go)
            {
                    GO.SetActive(true);
            }

            foreach(GameObject jointPoseScript in jointPoseGO)
            {
                var jps = jointPoseScript.GetComponent<JointPose>();
                jps.enabled = true;
            }
        }


    }


}
