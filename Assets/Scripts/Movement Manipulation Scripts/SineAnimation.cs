using UnityEngine;

public class SineAnimation : EventHandler
{
    [Header("Sine Wave properties")]
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    private float elapsedTime = 0;
    [SerializeField] private float heightOffset;

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
        float y = amplitude * Mathf.Sin(speed * elapsedTime);
        transform.position = new Vector3(transform.position.x,
            y + heightOffset,
            transform.position.z);

        elapsedTime += Time.deltaTime;
    }

    public void KillTrigger()
    {
        this.enabled = false;
    }
}
