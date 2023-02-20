using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPointCounter : MonoBehaviour
{
    public TMP_Text pointsText;
    public GameObject Canvas;
    public float targetPoints;

    private void Awake()
    {

        StartCoroutine(FloatUp_CR());
        StartCoroutine(UpdateText_CR());
    }

    private IEnumerator FloatUp_CR()
    {
        float t = 0;
        float maxTime = 3f;
        while (t < maxTime)
        {
            transform.position = Vector3.Lerp(transform.position - new Vector3(0,0.0005f, 0), transform.position + new Vector3(0, 0.002f, 0), t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    
    
    public IEnumerator UpdateText_CR()
    {
        float t = 0;
        float maxTime = 1.5f;

        while(t < maxTime)
        {
            
            pointsText.text = Mathf.RoundToInt(Mathf.Lerp(0, targetPoints, t/maxTime)).ToString();
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        pointsText.text = targetPoints.ToString();

        Invoke("DestroyObject", 0.7f);
    }


    
}
