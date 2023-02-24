using Mirror;
using UnityEngine;
using System;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITimer : NetworkBehaviour
{
    [SerializeField] private StartMatchService matchStarter;
    private TextMeshProUGUI timeText;

    [SyncVar]
    private float time;

    void Start()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        if (!matchStarter) return;
        if(isServer)
            matchStarter.startUICountdown.AddListener(ExecuteCountdown);
    }

    private void Update()
    {
        timeText.text = Mathf.RoundToInt(time).ToString();
    }

    private void ExecuteCountdown(float matchTime)
    {
        Debug.LogError("Executing");
        StartCoroutine(Countdown(matchTime));
    }

    private IEnumerator Countdown(float countdown)
    {
        while(countdown > 0)
        {
            countdown -= Time.deltaTime;
            time = countdown;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        timeText.text = "0";
        
    }
    private void OnDestroy()
    {
        matchStarter.startUICountdown.RemoveListener(ExecuteCountdown);
    }
}
