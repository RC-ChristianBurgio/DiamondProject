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

    void Start()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        if(matchStarter != null)
        {
            matchStarter.startUICountdown.AddListener(ExecuteCountdown);
        }
    }

    private void ExecuteCountdown(float matchTime)
    {
        StartCoroutine(Countdown(matchTime));
    }

    private IEnumerator Countdown(float countdown)
    {
        float endTime = 0;
        while(countdown > endTime)
        {
            timeText.text = Mathf.RoundToInt(countdown).ToString();
            countdown -= Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
        timeText.text = "0";
        
    }
    private void OnDestroy()
    {
        matchStarter.startUICountdown.RemoveListener(ExecuteCountdown);
    }
}
