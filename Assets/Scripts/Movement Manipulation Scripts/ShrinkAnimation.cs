using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ShrinkAnimation : EventHandler
{

    public override void Start()
    {
        base.Start();
        objectController.onShrink.AddListener(ShrinkTrigger);
    }

    private void OnDestroy()
    {
        objectController.onShrink.RemoveListener(ShrinkTrigger);
    }

    public IEnumerator Shrink(float duration)
    {
        float totalTime = 0;

        while (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            Vector3 newScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), Time.deltaTime);
            transform.localScale = newScale;
            yield return new WaitForEndOfFrame();
        }
        
        //Note: Let's give the responsibility of destroying to the controller
        //Destroy(this.gameObject);
    }

    public void ShrinkTrigger(float timeToDestroy)
    {
        StartCoroutine(Shrink(timeToDestroy));
    }
}
