using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartMatchService : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float matchTime;

    [HideInInspector] public UnityEvent<float> startUICountdown = new UnityEvent<float>();

    [HideInInspector] public UnityEvent spawnerStart = new UnityEvent();

    private void OnEnable()
    {
    //    startUICountdown?.Invoke(matchTime);
    //    spawnerStart?.Invoke();
    }

    public void SendMatchData()
    {
        startUICountdown?.Invoke(matchTime);
        spawnerStart?.Invoke();
    }


}
