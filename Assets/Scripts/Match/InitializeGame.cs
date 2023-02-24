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
        
        if(HandManager.Instance.GetHandGesture(false) == HandManager.GestureType.ThumbUp || Input.GetKeyDown(KeyCode.S))
        {
            matchService.SendMatchData();
            matchStarted = true;
        }
    }
}
