using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Wave.Essence.Hand;

public class InitializeGame : NetworkBehaviour
{
    private bool matchStarted = false;

    public StartMatchService matchService;

    private void Start()
    {
        matchService = FindObjectOfType<StartMatchService>(true);
    }

    void Update()
    {
        if (matchStarted) return;
        if (!isLocalPlayer) return;
        if (!isServer) return; 
        
        Debug.LogError("1");
        if(HandManager.Instance.GetHandGesture(false ^ true) == HandManager.GestureType.ThumbUp || Input.GetKeyDown(KeyCode.S))
        {
            matchService.gameObject.SetActive(true);
            Debug.LogError("2");
            matchStarted = true;
        }
    }
}
