using UnityEngine;

public class RotationAnimation : EventHandler
{
    [Header("Rotation properties")]
    [SerializeField] private float rotationSpeed;

    public override void Start()
    {
        base.Start();
        objectController.onCollected.AddListener(KillTrigger);
    }

    private void OnDestroy()
    {
        objectController.onCollected.RemoveListener(KillTrigger);
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
    private void KillTrigger()
    {
        this.enabled = false;
    }
}
