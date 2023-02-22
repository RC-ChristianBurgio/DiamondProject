using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnimation : EventHandler
{
    [SerializeField] private float blinkTime = 0.3f;
    private MeshRenderer objectRenderer;
    private bool invisible = false;

    public override void Start()
    {
        base.Start();
        objectRenderer = objectController.GetComponent<MeshRenderer>();
        objectController.onExpiry.AddListener(BlinkTrigger);
    }

    private void BlinkTrigger() {
        InvokeRepeating(nameof(Blink), 0.1f, blinkTime);
    }

    private void Blink()
    {
         objectRenderer.enabled = invisible;
         invisible = !invisible;
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

}
